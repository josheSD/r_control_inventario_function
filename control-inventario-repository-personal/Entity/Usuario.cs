using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace control_inventario_repository_personal.Entity
{
    [Table("usuario", Schema = "personal")]
    public partial class Usuario
    {

        [Key]
        [Column("usu__id")]
        public int UsuId { get; set; }

        [Required]
        [Column("usu__nombre")]
        [StringLength(255)]
        public string UsuNombre { get; set; }

        [Required]
        [Column("usu_apellidos")]
        [StringLength(255)]
        public string UsuApellidos { get; set; }

        [Required]
        [Column("usu__usuario")]
        [StringLength(255)]
        public string UsuUsuario { get; set; }

        [Required]
        [Column("usu__contrasenia")]
        [StringLength(255)]
        public string UsuContrasenia { get; set; }

        [Column("usu__imagen_url")]
        [StringLength(255)]
        public string? UsuImagenUrl { get; set; }

        [Column("usu__estado")]
        public int UsuEstado { get; set; }

        [Column("usu__rol_id")]
        public int UsuRolId { get; set; }

        [ForeignKey(nameof(UsuRolId))]
        [InverseProperty(nameof(Rol.Usuario))]
        public virtual Rol UsuRol { get; set; }

    }
}
