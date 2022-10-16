using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace control_inventario_repository_inventario.Entity
{
    [Table("articulo_almacen", Schema = "inventario")]
    public partial class ArticuloAlmacen
    {
        public ArticuloAlmacen()
        {

        }

        [Key]
        [Column("art_alm__id")]
        public int ArtAlmId { get; set; }

        [Column("art_alm__cantidad")]
        public int ArtAlmCantidad { get; set; }

        [Column("art_alm__estado")]
        public int ArtAlmEstado { get; set; }


        [Column("art_alm__art_id")]
        public int ArtAlmArtId { get; set; }


        [Column("art_alm__alm_id")]
        public int ArtAlmAlmId { get; set; }


        [ForeignKey(nameof(ArtAlmArtId))]
        [InverseProperty(nameof(Articulo.ArticuloAlmacen))]
        public virtual Articulo ArtAlmArt { get; set; }

        [ForeignKey(nameof(ArtAlmAlmId))]
        [InverseProperty(nameof(Almacen.ArticuloAlmacen))]
        public virtual Almacen ArtAlmAlm { get; set; }

    }
}
