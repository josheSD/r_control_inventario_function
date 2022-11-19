using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace control_inventario_service_inventario.ServiceDto
{
    public class PrecisionInventarioDto
    {
        public int IdAlmacen { get; set; }
        public string Almacen { get; set; }
        public List<PrecInvArticuloDTo> Articulos { get; set; }
    }

    public class PrecInvArticuloDTo
    {
        public int IdArticulo { get; set; }
        public string NombreArticulo { get; set; }
        public int TotalAnterior { get; set; }
        public int TotalActual { get; set; }
        public string Precision { get; set; }
    }
}
