using control_inventario_service_personal.ServiceDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace control_inventario_service_personal.service
{
    public interface IUsuarioService
    {

        Task<List<UsuarioDto>> Listar();
        Task Guardar(UsuarioDto usuario);
        Task Actualizar(UsuarioDto usuario);
        Task Eliminar(int idUsuario);

    }
}
