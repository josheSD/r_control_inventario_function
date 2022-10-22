using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace control_inventario_service_inventario.ServiceDto
{
    public class ProyectoVigenteDto
    {
        public int Id { get; set; }
        public List<ArticuloVigenteDto> Articulo { get; set; }
    }
}
