using control_inventario_service_inventario.ServiceDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace control_inventario_service_inventario.Service
{
    public interface IAlmacenService
    {
        Task<List<AlmacenDto>> Lista();
        Task Guardar(AlmacenDto almacen);
        Task Actualizar(AlmacenDto almacen);
        Task Eliminar(int idAlmacen);

    }
}
