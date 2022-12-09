using control_inventario_function.Soporte;
using control_inventario_repository_inventario.Context;
using control_inventario_repository_inventario.Entity;
using control_inventario_service_inventario.ServiceDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static control_inventario_function.SoporteEnum.Enums;

namespace control_inventario_service_inventario.Service.Imp
{
    public class AlmacenService : IAlmacenService
    {
        private readonly ControlInventarioContext context;
        public AlmacenService(ControlInventarioContext context)
        {
            this.context = context;
        }

        public async Task Actualizar(AlmacenDto almacen)
        {

            bool noSonIguales = false;

            var almacenBD = await context.Almacen
                                        .Where(e => e.AlmId == almacen.Id)
                                        .FirstOrDefaultAsync();

            if (almacenBD == null)
            {
                throw new CustomException("Almacen no encontrado");
            }

            // Limpiando

            var almacenAntiguaBD = await context.ArticuloAlmacen
                    .Where(x => x.ArtAlmAlmId == almacen.Id && x.ArtAlmEstado == (int)EstadoArticuloAlmacen.Anterior).ToListAsync();

            if (almacenAntiguaBD.Count() == almacen.Articulo.Count())
            {

                for (int i = 0; i < almacenAntiguaBD.Count(); i++)
                {
                    almacenAntiguaBD[i].ArtAlmEstado = (int)EstadoArticuloAlmacen.Antigua;
                    context.ArticuloAlmacen.Update(almacenAntiguaBD[i]);
                    await context.SaveChangesAsync();
                }

                var almacenAntiguaBD2 = await context.ArticuloAlmacen
                        .Where(x => x.ArtAlmAlmId == almacen.Id && x.ArtAlmEstado == (int)EstadoArticuloAlmacen.Activo).ToListAsync();

                for (int i = 0; i < almacenAntiguaBD2.Count(); i++)
                {
                    almacenAntiguaBD2[i].ArtAlmEstado = (int)EstadoArticuloAlmacen.Anterior;
                    context.ArticuloAlmacen.Update(almacenAntiguaBD2[i]);
                    await context.SaveChangesAsync();
                }

            }
            else
            {
                noSonIguales = true;
            }

            // Terminado

            almacenBD.AlmNombre = almacen.Nombre;
            almacenBD.AlmDireccion = almacen.Direccion;
            almacenBD.AlmFechaActualizacion = DateTime.UtcNow;

            context.Almacen.Update(almacenBD);
            await context.SaveChangesAsync();


            // Validando
            if (noSonIguales)
            {

                var almacenAntiguaBD2 = await context.ArticuloAlmacen
                        .Where(x => x.ArtAlmAlmId == almacen.Id && (x.ArtAlmEstado == (int)EstadoArticuloAlmacen.Anterior ||
                                                                    x.ArtAlmEstado == (int)EstadoArticuloAlmacen.Activo)).ToListAsync();

                for (int j = 0; j < almacenAntiguaBD2.Count(); j++)
                {
                    almacenAntiguaBD2[j].ArtAlmEstado = (int)EstadoArticuloAlmacen.Antigua;
                    context.ArticuloAlmacen.Update(almacenAntiguaBD2[j]);
                    await context.SaveChangesAsync();
                }


                for (int i = 0; i < almacen.Articulo.Count(); i++)
                {

                    ArticuloAlmacen newArticuloAlmacen = new ArticuloAlmacen();
                    newArticuloAlmacen.ArtAlmCantidad = almacen.Articulo[i].Cantidad;
                    newArticuloAlmacen.ArtAlmEstado = (int)EstadoArticuloAlmacen.Anterior;
                    newArticuloAlmacen.ArtAlmArtId = almacen.Articulo[i].Id;
                    newArticuloAlmacen.ArtAlmAlmId = almacenBD.AlmId;

                    await context.ArticuloAlmacen.AddAsync(newArticuloAlmacen);
                    await context.SaveChangesAsync();

                    ArticuloAlmacen newArticuloAlmacenCopied = new ArticuloAlmacen();
                    newArticuloAlmacenCopied.ArtAlmCantidad = almacen.Articulo[i].Cantidad;
                    newArticuloAlmacenCopied.ArtAlmEstado = (int)EstadoArticuloAlmacen.Activo;
                    newArticuloAlmacenCopied.ArtAlmArtId = almacen.Articulo[i].Id;
                    newArticuloAlmacenCopied.ArtAlmAlmId = almacenBD.AlmId;

                    await context.ArticuloAlmacen.AddAsync(newArticuloAlmacenCopied);
                    await context.SaveChangesAsync();

                }

            }
            else
            {
                for (int i = 0; i < almacen.Articulo.Count(); i++)
                {
                    ArticuloAlmacen newArticuloAlmacen = new ArticuloAlmacen();
                    newArticuloAlmacen.ArtAlmCantidad = almacen.Articulo[i].Cantidad;
                    newArticuloAlmacen.ArtAlmEstado = (int)EstadoArticuloAlmacen.Activo;
                    newArticuloAlmacen.ArtAlmArtId = almacen.Articulo[i].Id;
                    newArticuloAlmacen.ArtAlmAlmId = almacenBD.AlmId;

                    await context.ArticuloAlmacen.AddAsync(newArticuloAlmacen);
                    await context.SaveChangesAsync();
                }

            }


        }

        public async Task Eliminar(int idAlmacen)
        {

            var almacenBD = await context.Almacen.Where(e => e.AlmId == idAlmacen).FirstOrDefaultAsync();
            if (almacenBD == null)
            {
                throw new CustomException("Almacen no encontrado");
            }

            almacenBD.AlmEstado = (int)EstadoArticulo.Inactivo;

            context.Almacen.Update(almacenBD);
            await context.SaveChangesAsync();

        }

        public async Task Guardar(AlmacenDto almacen)
        {

            Almacen newAlmacen = new Almacen();
            newAlmacen.AlmNombre = almacen.Nombre;
            newAlmacen.AlmDireccion = almacen.Direccion;
            newAlmacen.AlmEstado = (int)EstadoAlmacen.Activo;
            newAlmacen.AlmFechaCreacion = DateTime.UtcNow;
            newAlmacen.AlmFechaActualizacion = DateTime.UtcNow;

            await context.Almacen.AddAsync(newAlmacen);
            await context.SaveChangesAsync();

            for (int i = 0; i < almacen.Articulo.Count(); i++)
            {
                ArticuloAlmacen newArticuloAlmacen = new ArticuloAlmacen();
                newArticuloAlmacen.ArtAlmCantidad = almacen.Articulo[i].Cantidad;
                newArticuloAlmacen.ArtAlmEstado = (int)EstadoArticuloAlmacen.Anterior;
                newArticuloAlmacen.ArtAlmArtId = almacen.Articulo[i].Id;
                newArticuloAlmacen.ArtAlmAlmId = newAlmacen.AlmId;

                await context.ArticuloAlmacen.AddAsync(newArticuloAlmacen);
                await context.SaveChangesAsync();


                ArticuloAlmacen newArticuloAlmacen2 = new ArticuloAlmacen();
                newArticuloAlmacen2.ArtAlmCantidad = almacen.Articulo[i].Cantidad;
                newArticuloAlmacen2.ArtAlmArtId = almacen.Articulo[i].Id;
                newArticuloAlmacen2.ArtAlmAlmId = newAlmacen.AlmId;
                newArticuloAlmacen2.ArtAlmEstado = (int)EstadoArticuloAlmacen.Activo;
                await context.ArticuloAlmacen.AddAsync(newArticuloAlmacen2);
                await context.SaveChangesAsync();
            }

        }

        public async Task<List<AlmacenDto>> Lista()
        {
            var lista = await context.Almacen
                    .Include(x => x.ArticuloAlmacen)
                    .Where(x => x.AlmEstado == (int)EstadoAlmacen.Activo)
                        .Select(e => new AlmacenDto
                        {
                            Id = e.AlmId,
                            Nombre = e.AlmNombre,
                            Direccion = e.AlmDireccion,
                            Estado = e.AlmEstado,
                            FechaCreacion = e.AlmFechaCreacion,
                            FechaActualizacion = e.AlmFechaActualizacion,
                            Articulo = e.ArticuloAlmacen
                            .Where(artAlm => artAlm.ArtAlmArtId == artAlm.ArtAlmArt.ArtId
                                          && artAlm.ArtAlmEstado == (int)EstadoArticuloAlmacen.Activo)
                                .Select(art => new ArticuloDto
                                {
                                    Id = art.ArtAlmArt.ArtId,
                                    Nombre = art.ArtAlmArt.ArtNombre,
                                    Url = art.ArtAlmArt.ArtUrl,
                                    Precio = art.ArtAlmArt.ArtPrecio,
                                    Estado = art.ArtAlmArt.ArtEstado,
                                    Cantidad = art.ArtAlmCantidad,
                                    Categoria = new CategoriaDto
                                    {
                                        Id = art.ArtAlmArt.ArtCat.CatId,
                                        Nombre = art.ArtAlmArt.ArtCat.CatNombre
                                    }
                                }).ToList()
                        }).ToListAsync();
            return lista;
        }
    }
}
