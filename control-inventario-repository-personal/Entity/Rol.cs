using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace control_inventario_repository_personal.Entity
{
    [Table("rol",Schema = "personal")]
    public partial class Rol
    {
        public Rol()
        {
            Usuario = new HashSet<Usuario>();
            RolMenu = new HashSet<RolMenu>();
        }

        [Key]
        [Column("rol__id")]
        public int RolId { get; set; }

        [Required]
        [Column("rol__nombre")]
        [StringLength(255)]
        public string RolNombre { get; set; }

        [InverseProperty("UsuRol")]
        public virtual ICollection<Usuario>  Usuario { get; set; }

        [InverseProperty("RolMenuRol")]
        public virtual ICollection<RolMenu> RolMenu { get; set; }
    }
}
