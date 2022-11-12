using Azure.Storage.Blobs;
using control_inventario_function.Soporte;
using control_inventario_function.SoporteUtil;
using control_inventario_repository_inventario.Context;
using control_inventario_repository_inventario.Entity;
using control_inventario_service_inventario.ServiceDto;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static control_inventario_function.SoporteEnum.Enums;

namespace control_inventario_service_inventario.Service.Imp
{
    public class ArticuloService : IArticuloService
    {
        private readonly ControlInventarioContext context;
        public ArticuloService(ControlInventarioContext context)
        {
            this.context = context;
        }
        public async Task Actualizar(ArticuloDto articulo)
        {
            var articuloBD = await context.Articulo.Where(e => e.ArtId == articulo.Id).FirstOrDefaultAsync();
            if (articuloBD == null)
            {
                throw new CustomException("Articulo no encontrado");
            }

            articuloBD.ArtNombre = articulo.Nombre;
            articuloBD.ArtUrl = articulo.Url;
            articuloBD.ArtPrecio = articulo.Precio;
            articuloBD.ArtFechaActualizacion = DateTime.UtcNow;
            articuloBD.ArtCatId = articulo.IdCategoria;

            context.Articulo.Update(articuloBD);
            await context.SaveChangesAsync();
        }

        public async Task Eliminar(int idArticulo)
        {
            var articuloBD = await context.Articulo
                    .Where(e => e.ArtId == idArticulo).FirstOrDefaultAsync();
            if (articuloBD == null)
            {
                throw new CustomException("Articulo no encontrado");
            }

            articuloBD.ArtEstado = (int)EstadoArticulo.Inactivo;

            context.Articulo.Update(articuloBD);
            await context.SaveChangesAsync();
        }

        public async Task Guardar(ArticuloDto articulo)
        {
            Articulo newArticulo = new Articulo();
            newArticulo.ArtNombre = articulo.Nombre;
            newArticulo.ArtUrl = articulo.Url;
            newArticulo.ArtPrecio = articulo.Precio;
            newArticulo.ArtEstado = (int)EstadoArticulo.Activo;
            newArticulo.ArtFechaCreacion = DateTime.UtcNow;
            newArticulo.ArtFechaActualizacion = DateTime.UtcNow;
            newArticulo.ArtCatId = articulo.IdCategoria;

            await context.Articulo.AddAsync(newArticulo);
            await context.SaveChangesAsync();
        }

        public async Task GuardarImagen(IFormFile fileImagen, string fileNombre)
        {
            var blobStorageConnectionString = SettingEnvironment.GetBlobStorageConnectionString();
            var blobStorageContainerName = SettingEnvironment.GetBlobStorageContainerName();

            var container = new BlobContainerClient(blobStorageConnectionString, blobStorageContainerName);
            var blob = container.GetBlobClient(fileNombre);
            var stream = fileImagen.OpenReadStream();
            await blob.UploadAsync(stream);
        }

        public async Task<List<ArticuloDto>> Lista()
        {
            var lista = await context.Articulo
                                .Include(x => x.ArtCat)
                                    .Where(x => x.ArtEstado == (int)EstadoArticulo.Activo)
                                    .Select(e => new ArticuloDto
                                    {
                                        Id = e.ArtId,
                                        Nombre = e.ArtNombre,
                                        Url = e.ArtUrl,
                                        Precio = e.ArtPrecio,
                                        Estado = e.ArtEstado,
                                        IdCategoria = e.ArtCatId,
                                        FechaCreacion = e.ArtFechaCreacion,
                                        FechaActualizacion = e.ArtFechaActualizacion,
                                        Categoria = new CategoriaDto
                                        {
                                            Id = e.ArtCat.CatId,
                                            Nombre = e.ArtCat.CatNombre
                                        }
                                    }).ToListAsync();
            return lista;
        }
    }
}
