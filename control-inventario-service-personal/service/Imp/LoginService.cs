using control_inventario_function.Soporte;
using control_inventario_repository_personal.Context;
using control_inventario_service_personal.ServiceDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
                                    .Where(e => e.UsuUsuario.Equals(login.User) && e.UsuContrasenia.Equals(login.Password))
                                    .FirstOrDefaultAsync();
            if (userDB == null)
            {
                throw new CustomException("Usuario o Contraseña incorrecta");
            }

            if (userDB.UsuEstado == (int)EstadoUsuario.Inactivo)
            {
                throw new CustomException("Usuario sin permisos");
            }

            var data = await context.Usuario
            .Include(x => x.UsuRol)
                .ThenInclude(x => x.RolMenu)
                    .ThenInclude(x => x.RolMenMen)
                        .ThenInclude(x => x.RolMenu)
            .Where(e => e.UsuUsuario.Equals(login.User) && e.UsuContrasenia.Equals(login.Password))
            .Select(lon => new LoginDto()
            {
                Nombre = lon.UsuNombre,
                Direccion = lon.UsuApellidos,
                Rol = lon.UsuRol.RolNombre,
                Usuario = lon.UsuUsuario,
                Token = "asfjewifjewiofjiowjefiojweiofjqioewuruqweiopruqioweurioqpweuiorquwieoruiqwe"
            }).FirstOrDefaultAsync();
            return data;
        }
    }
}
