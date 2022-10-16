using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace control_inventario_repository_inventario.Entity
{
    [Table("proyecto_almacen", Schema = "inventario")]
    public partial class ProyectoAlmacen
    {
        public ProyectoAlmacen()
        {

        }

        [Key]
        [Column("pro_alm__id")]
        public int ProAlmId { get; set; }

        [Column("pro_alm__cantidad")]
        public int ProAlmCantidad { get; set; }

        [Column("pro_alm__estado")]
        public int ProAlmEstado { get; set; }


        [Column("pro_alm__pro_id")]
        public int ProAlmProId { get; set; }


        [Column("pro_alm__alm_id")]
        public int ProAlmAlmId { get; set; }


        [Column("pro_alm__art_id")]
        public int ProAlmArtId { get; set; }


        [ForeignKey(nameof(ProAlmProId))]
        [InverseProperty(nameof(Proyecto.ProyectoAlmacen))]
        public virtual Proyecto ProAlmPro { get; set; }


        [ForeignKey(nameof(ProAlmAlmId))]
        [InverseProperty(nameof(Almacen.ProyectoAlmacen))]
        public virtual Almacen ProAlmAlm { get; set; }


    }
}
