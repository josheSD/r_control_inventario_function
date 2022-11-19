using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace control_inventario_service_inventario.ServiceDto
{
    public class RotacionInventarioDto
    {
        public int IdAlmacen { get; set; }
        public string Almacen { get; set; }
        public List<RotInvArticuloDTo> Articulos { get; set; }
    }

    public class RotInvArticuloDTo
    {
        public int IdArticulo { get; set; }
        public string NombreArticulo { get; set; }
        public int UnidadSalida { get; set; }
        public int UnidadStock { get; set; }
        public string Rotacion { get; set; }
    }
}
