using control_inventario_repository_personal.Context;
using control_inventario_service_personal.ServiceDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace control_inventario_service_personal.service.Imp
{
    public class RolService : IRolService
    {
        private readonly ControlInventarioContext context;

        public RolService(ControlInventarioContext context)
        {
            this.context = context;
        }
        public async Task<List<RolDto>> Listar()
        {
            var lista = await context.Rol.Select(e => new RolDto
            {
                Id = e.RolId,
                Nombre = e.RolNombre,
            }).ToListAsync();
            return lista;

        }
    }
}
