using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace control_inventario_repository_personal.Entity
{
    [Table("menu", Schema = "personal")]
    public partial class Menu
    {
        public Menu()
        {
            RolMenu = new HashSet<RolMenu>();
        }

        [Key]
        [Column("men__id")]
        public int MenId { get; set; }

        [Required]
        [Column("men__nombre")]
        [StringLength(255)]
        public string MenNombre { get; set; }

        [Column("men__icon")]
        [StringLength(255)]
        public string MenIcon { get; set; }

        [Column("men__link")]
        [StringLength(255)]
        public string MenLink { get; set; }

        [InverseProperty("RolMenMen")]
        public virtual ICollection<RolMenu> RolMenu { get; set; }
    }
}
