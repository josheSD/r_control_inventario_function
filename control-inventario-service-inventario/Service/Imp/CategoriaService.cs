using control_inventario_repository_inventario.Context;
using control_inventario_service_inventario.ServiceDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace control_inventario_service_inventario.Service.Imp
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ControlInventarioContext context;
        public CategoriaService(ControlInventarioContext context)
        {
            this.context = context;
        }

        public async Task<List<CategoriaDto>> Lista()
        {
            var lista = await context.Categoria.Select(e => new CategoriaDto
            {
                Id = e.CatId,
                Nombre = e.CatNombre,
            }).ToListAsync();
            return lista;
        }
    }
}
