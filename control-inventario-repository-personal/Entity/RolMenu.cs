using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace control_inventario_repository_personal.Entity
{
    [Table("rol_menu", Schema = "personal")]
    public partial class RolMenu
    {

        [Key]
        [Column("rol_men__id")]
        public int RolMenId { get; set; }

        [Column("rol_men__rol_id")]
        public int RolMenRolId { get; set; }

        [Column("rol_men__men_id")]
        public int RolMenMenId { get; set; }

        [ForeignKey(nameof(RolMenMenId))]
        [InverseProperty(nameof(Menu.RolMenu))]
        public virtual Menu RolMenMen { get; set; }

        [ForeignKey(nameof(RolMenRolId))]
        [InverseProperty(nameof(Rol.RolMenu))]
        public virtual Rol RolMenuRol { get; set; }


    }
}
