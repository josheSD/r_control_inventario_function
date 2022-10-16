using control_inventario_service_inventario.ServiceDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace control_inventario_service_inventario.Service
{
    public interface ICategoriaService
    {
        Task<List<CategoriaDto>> Lista();
    }
}
