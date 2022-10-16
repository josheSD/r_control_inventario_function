using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace control_inventario_service_personal.ServiceDto
{
    public class MenuDto
    {
        public int MenuId { get; set; }
        public string MenuNombre { get; set; }
        public string MenuIconUrl { get; set; }
        public string MenuLink { get; set; }
        public List<MenuDto> MenuHijo { get; set; }
    }
}
