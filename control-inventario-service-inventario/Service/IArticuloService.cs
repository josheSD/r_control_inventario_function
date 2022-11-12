using control_inventario_service_inventario.ServiceDto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace control_inventario_service_inventario.Service
{
    public interface IArticuloService
    {
        Task<List<ArticuloDto>> Lista();
        Task GuardarImagen(IFormFile fileImagen, string fileNombre);
        Task Guardar(ArticuloDto articulo);
        Task Actualizar(ArticuloDto articulo);
        Task Eliminar(int idArticulo);
    }
}
