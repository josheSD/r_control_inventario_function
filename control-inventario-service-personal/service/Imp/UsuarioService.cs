using control_inventario_function.Soporte;
using control_inventario_repository_personal.Context;
using control_inventario_repository_personal.Entity;
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
    public class UsuarioService : IUsuarioService
    {
        private readonly ControlInventarioContext context;

        public UsuarioService(ControlInventarioContext context)
        {
            this.context = context;
        }

        public async Task Actualizar(UsuarioDto usuario)
        {
            var usuarioBD = await context.Usuario.Where(e => e.UsuId == usuario.Id).FirstOrDefaultAsync();

            if (usuarioBD == null)
            {
                throw new CustomException("Usuario no encontrado");
            }

            usuarioBD.UsuId = usuario.Id ?? 0;
            usuarioBD.UsuNombre = usuario.Nombre;
            usuarioBD.UsuDireccion = usuario.Direccion;
            usuarioBD.UsuUsuario = usuario.Usuario;
            usuarioBD.UsuContrasenia = BCrypt.Net.BCrypt.HashPassword(usuario.Contrasenia);
            usuarioBD.UsuRolId = usuario.IdRol;

            context.Usuario.Update(usuarioBD);
            await context.SaveChangesAsync();
        }

        public async Task Eliminar(int idUsuario)
        {
            var usuarioBD = await context.Usuario.Where(e => e.UsuId == idUsuario).FirstOrDefaultAsync();
            if (usuarioBD == null)
            {
                throw new CustomException("Usuario no encontrado");
            }

            usuarioBD.UsuEstado = (int)EstadoUsuario.Inactivo;

            context.Usuario.Update(usuarioBD);
            await context.SaveChangesAsync();
        }

        public async Task Guardar(UsuarioDto usuario)
        {
            var usuarioNew = new Usuario
            {
                UsuNombre = usuario.Nombre,
                UsuDireccion = usuario.Direccion,
                UsuUsuario = usuario.Usuario,
                UsuContrasenia = BCrypt.Net.BCrypt.HashPassword(usuario.Contrasenia),
                UsuEstado = (int)EstadoUsuario.Activo,
                UsuRolId = usuario.IdRol,
            };
            await context.Usuario.AddAsync(usuarioNew);
            await context.SaveChangesAsync();
        }

        public async Task<List<UsuarioDto>> Listar()
        {
            var lista = context.Usuario.Select(e => new UsuarioDto
            {
                Id = e.UsuId,
                Nombre = e.UsuNombre,
                Direccion = e.UsuDireccion,
                Usuario = e.UsuUsuario,
                Contrasenia = e.UsuContrasenia,
                Rol = new RolDto
                {
                    Id = e.UsuRolId,
                    Nombre = e.UsuRol.RolNombre
                }
            }).ToList();
            return lista;
        }
    }
}
