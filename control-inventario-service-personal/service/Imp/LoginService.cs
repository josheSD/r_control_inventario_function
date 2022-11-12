using control_inventario_function.Soporte;
using control_inventario_function.SoporteUtil;
using control_inventario_repository_personal.Context;
using control_inventario_service_personal.ServiceDto;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static control_inventario_function.SoporteEnum.Enums;

namespace control_inventario_service_personal.service.Imp
{
    public class LoginService : ILoginService
    {
        private readonly ControlInventarioContext context;
        public LoginService(ControlInventarioContext context)
        {
            this.context = context;
        }

        public async Task<LoginDto> Login(RequestLogin login)
        {
            var userDB = await context.Usuario
                                    .Where(e => e.UsuUsuario.Equals(login.User))
                                    .FirstOrDefaultAsync();
            if (userDB == null)
            {
                throw new CustomException("Usuario no existe");
            }

            bool validPass = BCrypt.Net.BCrypt.Verify(login.Password, userDB.UsuContrasenia);

            if (!validPass)
            {
                throw new CustomException("Usuario o Contraseña incorrecta");
            }

            if (userDB.UsuEstado == (int)EstadoUsuario.Inactivo)
            {
                throw new CustomException("Usuario inactivo");
            }

            var token = GeneTokenJwt(login.User);

            var data = await context.Usuario
            .Include(x => x.UsuRol)
                .ThenInclude(x => x.RolMenu)
                    .ThenInclude(x => x.RolMenMen)
                        .ThenInclude(x => x.RolMenu)
            .Where(e => e.UsuUsuario.Equals(login.User))
            .Select(lon => new LoginDto()
            {
                Nombre = lon.UsuNombre,
                Direccion = lon.UsuDireccion,
                Usuario = lon.UsuUsuario,
                Rol = new RolDto
                {
                    Id = lon.UsuRol.RolId,
                    Nombre = lon.UsuRol.RolNombre
                },
                Token = token
            }).FirstOrDefaultAsync();
            return data;
        }
        private string GeneTokenJwt(string username)
        {
            var secretKey = SettingEnvironment.GetJWTSecretKey();
            var audienceToken = SettingEnvironment.GetJWTAudienceKey();
            var issuerToken = SettingEnvironment.GetJWTIssuerKey();
            var expireTime = SettingEnvironment.GetJWTExpireKey();

            Claim[] claims = new[]
                {
                    new Claim("Nombre",username),
                };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            int HorasExpiracion = Int32.Parse(expireTime);
            DateTime expiration = DateTime.UtcNow.AddHours(HorasExpiracion);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: issuerToken,
                audience: audienceToken,
                claims: claims,
                expires: expiration,
                signingCredentials: creds
                );

            string WriteToken = new JwtSecurityTokenHandler().WriteToken(token);

            return WriteToken;

        }
    }

}
