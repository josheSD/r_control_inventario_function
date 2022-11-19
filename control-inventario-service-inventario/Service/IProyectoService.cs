using control_inventario_service_inventario.ServiceDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace control_inventario_service_inventario.Service
{
    public interface IProyectoService
    {
        Task<List<ProyectoDto>> Lista();
        Task Guardar(ProyectoDto proyecto);
        Task Actualizar(ProyectoDto proyecto);
        Task Vigente(ProyectoVigenteDto proyecto);
        Task Eliminar(int idProyecto);
        Task<List<PrecisionInventarioDto>> PrecisionInventario();
        Task<List<RotacionInventarioDto>> RotacionInventario();
    }
}
