using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace control_inventario_repository_inventario.Entity
{
    [Table("articulo", Schema = "inventario")]
    public partial class Articulo
    {
        public Articulo()
        {
            ArticuloAlmacen = new HashSet<ArticuloAlmacen>();
        }

        [Key]
        [Column("art__id")]
        public int ArtId { get; set; }

        [Column("art__nombre")]
        [StringLength(255)]
        public string ArtNombre { get; set; }

        [Column("art__url")]
        [StringLength(255)]
        public string ArtUrl { get; set; }

        [Column("art__precio")]
        [StringLength(255)]
        public string ArtPrecio { get; set; }

        [Column("art__estado")]
        public int ArtEstado { get; set; }

        [Column("art__fecha_creacion", TypeName = "datetime")]
        public DateTime ArtFechaCreacion { get; set; }

        [Column("art__fecha_actualizacion", TypeName = "datetime")]
        public DateTime ArtFechaActualizacion { get; set; }

        [Column("art__cat_id")]
        public int ArtCatId { get; set; }

        [ForeignKey(nameof(ArtCatId))]
        [InverseProperty(nameof(Categoria.Articulo))]
        public virtual Categoria ArtCat { get; set; }



        [InverseProperty("ArtAlmArt")]
        public virtual ICollection<ArticuloAlmacen> ArticuloAlmacen { get; set; }
    }
}
