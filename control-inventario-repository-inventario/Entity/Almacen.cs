using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace control_inventario_repository_inventario.Entity
{
    [Table("almacen", Schema = "inventario")]
    public partial class Almacen
    {
        public Almacen()
        {
            ArticuloAlmacen = new HashSet<ArticuloAlmacen>();
            ProyectoAlmacen = new HashSet<ProyectoAlmacen>();
        }

        [Key]
        [Column("alm__id")]
        public int AlmId { get; set; }

        [Column("alm__nombre")]
        [StringLength(255)]
        public string AlmNombre { get; set; }

        [Column("alm__direccion")]
        [StringLength(255)]
        public string AlmDireccion { get; set; }

        [Column("alm__estado")]
        public int AlmEstado { get; set; }

        [Column("alm__fecha_creacion", TypeName = "datetime")]
        public DateTime AlmFechaCreacion { get; set; }

        [Column("alm__fecha_actualizacion", TypeName = "datetime")]
        public DateTime AlmFechaActualizacion { get; set; }


        [InverseProperty("ArtAlmAlm")]
        public virtual ICollection<ArticuloAlmacen> ArticuloAlmacen { get; set; }

        [InverseProperty("ProAlmAlm")]
        public virtual ICollection<ProyectoAlmacen> ProyectoAlmacen { get; set; }

        
    }
}
