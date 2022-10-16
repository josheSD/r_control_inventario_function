using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace control_inventario_service_inventario.ServiceDto
{
    public class CategoriaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
