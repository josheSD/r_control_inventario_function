using control_inventario_service_personal.ServiceDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace control_inventario_service_personal.service
{
    public interface ILoginService
    {
        Task<LoginDto> Login(RequestLogin login);
    }
}
