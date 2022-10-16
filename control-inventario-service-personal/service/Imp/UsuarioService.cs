using control_inventario_function.Soporte;
using control_inventario_repository_personal.Context;
using control_inventario_repository_personal.Entity;
using control_inventario_service_personal.ServiceDto;
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

        public async Task<UsuarioDto> Actualizar(UsuarioDto usuario)
        {
            var usuarioBD = context.Usuario.Where(e => e.UsuId == usuario.UsuId).FirstOrDefault();

            if (usuarioBD == null)
            {
                throw new CustomException("Usuario no encontrado");
            }

            usuarioBD.UsuId = usuario.UsuId ?? 0;
            usuarioBD.UsuApellidos = usuario.UsuApellidos;
            usuarioBD.UsuContrasenia = usuario.UsuContrasenia;
            usuarioBD.UsuImagenUrl = usuario.UsuImagenUrl;
            usuarioBD.UsuNombre = usuario.UsuNombre;
            usuarioBD.UsuRolId = usuario.UsuRolId;

            context.Usuario.Update(usuarioBD);
            await context.SaveChangesAsync();
            return usuario;
        }

        public async Task<UsuarioDto> Eliminar(int idUsuario)
        {
            var usuarioBD = context.Usuario.Where(e => e.UsuId == idUsuario).FirstOrDefault();
            if (usuarioBD == null)
            {
                throw new CustomException("Usuario no encontrado");
            }

            var usuario = new UsuarioDto()
            {
                UsuId = usuarioBD.UsuId,
                UsuApellidos = usuarioBD.UsuApellidos,
                UsuContrasenia = usuarioBD.UsuContrasenia,
                UsuImagenUrl = usuarioBD.UsuImagenUrl,
                UsuNombre = usuarioBD.UsuNombre,
                UsuRolId = usuarioBD.UsuRolId,
                UsuUsuario = usuarioBD.UsuUsuario,
                UsuEstado = usuarioBD.UsuEstado == (int)EstadoUsuario.Activo ? true : false
            };

            usuarioBD.UsuEstado = (int)EstadoUsuario.Inactivo;

            context.Usuario.Update(usuarioBD);
            await context.SaveChangesAsync();

            return usuario;
        }

        public async Task<UsuarioDto> Guardar(UsuarioDto usuario)
        {
            var usuarioNew = new Usuario
            {
                UsuApellidos = usuario.UsuApellidos,
                UsuContrasenia = usuario.UsuContrasenia,
                UsuImagenUrl = usuario.UsuImagenUrl,
                UsuNombre = usuario.UsuNombre,
                UsuRolId = usuario.UsuRolId,
                UsuUsuario = usuario.UsuUsuario,
                UsuEstado = (int)EstadoUsuario.Activo
            };
            await context.Usuario.AddAsync(usuarioNew);
            await context.SaveChangesAsync();

            usuario.UsuEstado = true;
            usuario.UsuId = usuarioNew.UsuId;

            return usuario;
        }

        public async Task<List<UsuarioDto>> Listar()
        {
            var lista = context.Usuario.Select(e => new UsuarioDto
            {
                UsuId = e.UsuId,
                UsuApellidos = e.UsuApellidos,
                UsuContrasenia = e.UsuContrasenia,
                UsuImagenUrl = e.UsuImagenUrl,
                UsuNombre = e.UsuNombre,
                UsuUsuario = e.UsuUsuario,
                UsuRolId = e.UsuRolId,
                UsuRolNombre = e.UsuRol.RolNombre,
                UsuEstado = e.UsuEstado == (int)EstadoUsuario.Activo ? true : false
            }).ToList();
            return lista;
        }
    }
}
