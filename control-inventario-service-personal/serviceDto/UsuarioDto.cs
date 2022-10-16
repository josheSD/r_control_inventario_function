using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace control_inventario_service_personal.ServiceDto
{
    public class UsuarioDto
    {

        public int? UsuId { get; set; }
        public string UsuNombre { get; set; }
        public string UsuApellidos { get; set; }
        public string UsuUsuario { get; set; }
        public string UsuContrasenia { get; set; }
        public string? UsuImagenUrl { get; set; }
        public int UsuRolId { get; set; }
        public string? UsuRolNombre { get; set; }
        public bool UsuEstado { get; set; }

    }
}
