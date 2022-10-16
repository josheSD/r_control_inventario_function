using control_inventario_service_personal.ServiceDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace control_inventario_service_personal.service
{
    public interface IUsuarioService
    {

        Task<List<UsuarioDto>> Listar();
        Task<UsuarioDto> Guardar(UsuarioDto usuario);
        Task<UsuarioDto> Actualizar(UsuarioDto usuario);
        Task<UsuarioDto> Eliminar(int idUsuario);

    }
}
