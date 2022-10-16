using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace control_inventario_repository_inventario.Entity
{
    [Table("proyecto", Schema = "inventario")]
    public partial class Proyecto
    {
        public Proyecto()
        {
            ProyectoAlmacen = new HashSet<ProyectoAlmacen>();
        }

        [Key]
        [Column("pro__id")]
        public int ProId { get; set; }

        [Column("pro__nombre")]
        [StringLength(255)]
        public string ProNombre { get; set; }

        [Column("pro__cliente")]
        [StringLength(255)]
        public string ProCliente { get; set; }

        [Column("pro__fecha_inicio", TypeName = "datetime")]
        public DateTime ProFechaInicio { get; set; }

        [Column("pro__fecha_fin", TypeName = "datetime")]
        public DateTime ProFechaFin { get; set; }

        [Column("pro__estado")]
        public int ProEstado { get; set; }

        [Column("pro__fecha_creacion", TypeName = "datetime")]
        public DateTime ProFechaCreacion { get; set; }

        [Column("pro__fecha_actualizacion", TypeName = "datetime")]
        public DateTime ProFechaActualizacion { get; set; }


        [InverseProperty("ProAlmPro")]
        public virtual ICollection<ProyectoAlmacen> ProyectoAlmacen { get; set; }

    }
}
