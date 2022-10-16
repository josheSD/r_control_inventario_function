using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace control_inventario_repository_inventario.Entity
{
    [Table("categoria", Schema = "inventario")]
    public partial class Categoria
    {
        public Categoria()
        {
            Articulo = new HashSet<Articulo>();
        }

        [Key]
        [Column("cat__id")]
        public int CatId { get; set; }

        [Column("cat__nombre")]
        [StringLength(255)]
        public string CatNombre { get; set; }

        [InverseProperty("ArtCat")]
        public virtual ICollection<Articulo> Articulo { get; set; }
    }
}
