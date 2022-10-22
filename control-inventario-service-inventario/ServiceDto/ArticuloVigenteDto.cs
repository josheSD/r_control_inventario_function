using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace control_inventario_service_inventario.ServiceDto
{
    public class ArticuloVigenteDto
    {
        public int Id { get; set; }
        public int Cantidad { get; set; }
        public int Buena { get; set; }
        public int Daniado { get; set; }
        public int IdAlmacen { get; set; }
    }
}
