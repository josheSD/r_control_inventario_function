using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace control_inventario_service_inventario.ServiceDto
{
    public class ArticuloDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Url { get; set; }
        public string Precio { get; set; }
        public int Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public int IdCategoria { get; set; }

        public int IdAlmacen { get; set; }

        public int Cantidad { get; set; }

        public CategoriaDto Categoria { get; set; }
        public AlmacenDto Almacen { get; set; }
    }
}
