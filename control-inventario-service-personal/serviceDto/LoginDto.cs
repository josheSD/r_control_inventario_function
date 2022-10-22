using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace control_inventario_service_personal.ServiceDto
{
    public class LoginDto
    {
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Usuario { get; set; }
        public string Contrasenia { get; set; }
        public RolDto Rol { get; set; }
        public string Token { get; set; }

    }
}
