using control_inventario_function.Soporte;
using control_inventario_repository_inventario.Context;
using control_inventario_repository_inventario.Entity;
using control_inventario_service_inventario.ServiceDto;
using Microsoft.Azure.Documents;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static control_inventario_function.SoporteEnum.Enums;

namespace control_inventario_service_inventario.Service.Imp
{
    public class ProyectoService : IProyectoService
    {
        private readonly ControlInventarioContext context;
        public ProyectoService(ControlInventarioContext context)
        {
            this.context = context;
        }

        public async Task Actualizar(ProyectoDto proyecto)
        {
            // Limpiando

            var proyectoAntiguaBD = await context.ProyectoAlmacen
                    .Where(x => x.ProAlmProId == proyecto.Id && x.ProAlmEstado == (int)EstadoProyectoAlmacen.Activo).ToListAsync();

            for (int i = 0; i < proyectoAntiguaBD.Count(); i++)
            {
                proyectoAntiguaBD[i].ProAlmEstado = (int)EstadoProyectoAlmacen.Antigua;
                context.ProyectoAlmacen.Update(proyectoAntiguaBD[i]);
                await context.SaveChangesAsync();
            }

            // Terminado

            var proyectoDB = await context.Proyecto
                                    .Where(e => e.ProId == proyecto.Id)
                                    .FirstOrDefaultAsync();

            if (proyectoDB == null)
            {
                throw new CustomException("Proyecto no encontrado");
            }

            proyectoDB.ProNombre = proyecto.Nombre;
            proyectoDB.ProCliente = proyecto.Cliente;
            proyectoDB.ProFechaInicio = proyecto.FechaInicio;
            proyectoDB.ProFechaFin = proyecto.FechaFin;
            proyectoDB.ProFechaActualizacion = DateTime.UtcNow;


            context.Proyecto.Update(proyectoDB);
            await context.SaveChangesAsync();

            for (int i = 0; i < proyecto.Articulo.Count(); i++)
            {

                ProyectoAlmacen newProyectoAlmacen = new ProyectoAlmacen();
                newProyectoAlmacen.ProAlmCantidad = proyecto.Articulo[i].Cantidad;
                newProyectoAlmacen.ProAlmEstado = (int)EstadoProyectoAlmacen.Activo;
                newProyectoAlmacen.ProAlmProId = proyectoDB.ProId;
                newProyectoAlmacen.ProAlmAlmId = proyecto.Articulo[i].IdAlmacen;
                newProyectoAlmacen.ProAlmArtId = proyecto.Articulo[i].Id;

                await context.ProyectoAlmacen.AddAsync(newProyectoAlmacen);
                await context.SaveChangesAsync();

            }
        }

        public async Task Eliminar(int idProyecto)
        {
            var proyectoDB = await context.Proyecto
                    .Where(e => e.ProId == idProyecto).FirstOrDefaultAsync();

            if (proyectoDB == null)
            {
                throw new CustomException("Proyecto no encontrado");
            }

            proyectoDB.ProEstado = (int)EstadoProyecto.Inactivo;

            context.Proyecto.Update(proyectoDB);
            await context.SaveChangesAsync();
        }

        public async Task Guardar(ProyectoDto proyecto)
        {
            Proyecto newProyecto = new Proyecto();
            newProyecto.ProNombre = proyecto.Nombre;
            newProyecto.ProCliente = proyecto.Cliente;
            newProyecto.ProFechaInicio = proyecto.FechaInicio;
            newProyecto.ProFechaFin = proyecto.FechaFin;
            newProyecto.ProEstado = (int)EstadoProyecto.Vigente;
            newProyecto.ProFechaCreacion = DateTime.UtcNow;
            newProyecto.ProFechaActualizacion = DateTime.UtcNow;

            await context.Proyecto.AddAsync(newProyecto);
            await context.SaveChangesAsync();

            for (int i = 0; i < proyecto.Articulo.Count(); i++)
            {
                ProyectoAlmacen newProyectoAlmacen = new ProyectoAlmacen();
                newProyectoAlmacen.ProAlmCantidad = proyecto.Articulo[i].Cantidad;
                newProyectoAlmacen.ProAlmEstado = (int)EstadoProyectoAlmacen.Activo;
                newProyectoAlmacen.ProAlmProId = newProyecto.ProId;
                newProyectoAlmacen.ProAlmAlmId = proyecto.Articulo[i].IdAlmacen;
                newProyectoAlmacen.ProAlmArtId = proyecto.Articulo[i].Id;

                await context.ProyectoAlmacen.AddAsync(newProyectoAlmacen);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<ProyectoDto>> Lista()
        {
            var lista = await context.Proyecto
                .Include(x => x.ProyectoAlmacen)
                    .ThenInclude(x => x.ProAlmAlm)
                    .Where(x => x.ProEstado == (int)EstadoProyecto.Vigente || x.ProEstado == (int)EstadoProyecto.Concluido)
                    .Select(e => new ProyectoDto
                    {
                        Id = e.ProId,
                        Nombre = e.ProNombre,
                        Cliente = e.ProCliente,
                        Contrato = "Contrato X",
                        FechaInicio = e.ProFechaInicio,
                        FechaFin = e.ProFechaFin,
                        Estado = e.ProEstado,
                        FechaCreacion = e.ProFechaCreacion,
                        FechaActualizacion = e.ProFechaActualizacion,
                        Articulo = e.ProyectoAlmacen
                            .Where(proAlmacen => proAlmacen.ProAlmEstado == (int)EstadoProyectoAlmacen.Activo)
                            .Select(proAlm => new ArticuloDto
                            {
                                Id = context.Articulo.Where(art => proAlm.ProAlmArtId == art.ArtId).FirstOrDefault().ArtId,
                                Nombre = context.Articulo.Where(art => proAlm.ProAlmArtId == art.ArtId).FirstOrDefault().ArtNombre,
                                Url = context.Articulo.Where(art => proAlm.ProAlmArtId == art.ArtId).FirstOrDefault().ArtUrl,
                                Precio = context.Articulo.Where(art => proAlm.ProAlmArtId == art.ArtId).FirstOrDefault().ArtPrecio,
                                Estado = context.Articulo.Where(art => proAlm.ProAlmArtId == art.ArtId).FirstOrDefault().ArtEstado,
                                Cantidad = proAlm.ProAlmCantidad,
                                Categoria = new CategoriaDto
                                {
                                    Id = context.Articulo.Where(art => proAlm.ProAlmArtId == art.ArtId).FirstOrDefault().ArtCat.CatId,
                                    Nombre = context.Articulo.Where(art => proAlm.ProAlmArtId == art.ArtId).FirstOrDefault().ArtCat.CatNombre
                                },
                                Almacen = new AlmacenDto
                                {
                                    Id = proAlm.ProAlmAlm.AlmId,
                                    Nombre = proAlm.ProAlmAlm.AlmNombre,
                                    Direccion = proAlm.ProAlmAlm.AlmDireccion,
                                    Articulo = new List<ArticuloDto>()
                                }
                            }).ToList()
                    }).ToListAsync();
            return lista;
        }

        public async Task Vigente(ProyectoVigenteDto proyecto)
        {

            // Limpiando

            var proyectoAntiguaBD = await context.ProyectoAlmacen
                    .Where(x => x.ProAlmProId == proyecto.Id && x.ProAlmEstado == (int)EstadoProyectoAlmacen.Activo).ToListAsync();

            for (int i = 0; i < proyectoAntiguaBD.Count(); i++)
            {
                proyectoAntiguaBD[i].ProAlmEstado = (int)EstadoProyectoAlmacen.Antigua;
                context.ProyectoAlmacen.Update(proyectoAntiguaBD[i]);
                await context.SaveChangesAsync();
            }

            // Terminado

            var proyectoDB = await context.Proyecto
                                    .Where(e => e.ProId == proyecto.Id)
                                    .FirstOrDefaultAsync();

            if (proyectoDB == null)
            {
                throw new CustomException("Proyecto no encontrado");
            }

            proyectoDB.ProEstado = (int)EstadoProyecto.Concluido;
            proyectoDB.ProFechaActualizacion = DateTime.UtcNow;

            context.Proyecto.Update(proyectoDB);
            await context.SaveChangesAsync();

            for (int i = 0; i < proyecto.Articulo.Count(); i++)
            {

                ProyectoAlmacen newProyectoAlmacen = new ProyectoAlmacen();
                newProyectoAlmacen.ProAlmCantidad = proyecto.Articulo[i].Cantidad;
                newProyectoAlmacen.ProAlmEstado = (int)EstadoProyectoAlmacen.Activo;
                newProyectoAlmacen.ProAlmProId = proyectoDB.ProId;
                newProyectoAlmacen.ProAlmAlmId = proyecto.Articulo[i].IdAlmacen;
                newProyectoAlmacen.ProAlmArtId = proyecto.Articulo[i].Id;

                await context.ProyectoAlmacen.AddAsync(newProyectoAlmacen);
                await context.SaveChangesAsync();
            }


        }

        public async Task<List<PrecisionInventarioDto>> PrecisionInventario()
        {
            IQueryable<Almacen> listaData = context.Almacen
                                .Include(x => x.ArticuloAlmacen)
                                .Include(x => x.ProyectoAlmacen)
                        .Where(x => x.AlmEstado == (int)EstadoAlmacen.Activo).AsQueryable();

            var listaFinal = new List<PrecisionInventarioDto>();

            for (var i = 0; i < listaData.Count(); i++)
            {
                listaFinal.Add(new PrecisionInventarioDto
                {
                    IdAlmacen = listaData.ToList()[i].AlmId,
                    Almacen = listaData.ToList()[i].AlmNombre,
                    Articulos = this.getArticulosPrecision(listaData, i)
                }); ;
            }

            return listaFinal;
        }

        private List<PrecInvArticuloDTo> getArticulosPrecision(IQueryable<Almacen> lista, int index)
        {
            var listaNueva = new List<PrecInvArticuloDTo>();

            var listaArticulo = lista.ToList()[index].ArticuloAlmacen.Where(x => x.ArtAlmEstado == (int)EstadoArticuloAlmacen.Activo);
            var listaArticuloAnterior = lista.ToList()[index].ArticuloAlmacen.Where(x => x.ArtAlmEstado == (int)EstadoArticuloAlmacen.Anterior);

            for (var i = 0; i < listaArticulo.Count(); i++)
            {
                listaNueva.Add(new PrecInvArticuloDTo
                {
                    IdArticulo = listaArticulo.ToList()[i].ArtAlmArtId,
                    NombreArticulo = context.Articulo.Where(x => x.ArtId == listaArticulo.ToList()[i].ArtAlmArtId).FirstOrDefault().ArtNombre,
                    TotalAnterior = listaArticuloAnterior.ToList()[i].ArtAlmCantidad,
                    TotalActual = listaArticulo.ToList()[i].ArtAlmCantidad,
                    Precision = this.getPrecision(listaArticuloAnterior.ToList()[i].ArtAlmCantidad, listaArticulo.ToList()[i].ArtAlmCantidad)
                }
                );
            }


            return listaNueva;
        }

        private string getPrecision(int cantidadAnterior, int cantidadActual)
        {
            double calc = ((double)cantidadAnterior / (double)cantidadActual) * 100;
            string tpi = String.Format("{0:.##}", calc);
            return tpi;
        }

        public async Task<List<RotacionInventarioDto>> RotacionInventario()
        {
            IQueryable<Almacen> listaData = context.Almacen
                                .Include(x => x.ArticuloAlmacen)
                                .Include(x => x.ProyectoAlmacen)
                                    .ThenInclude(x => x.ProAlmPro)
                        .Where(x => x.AlmEstado == (int)EstadoAlmacen.Activo).AsQueryable();

            var listaFinal = new List<RotacionInventarioDto>();

            for (var i = 0; i < listaData.Count(); i++)
            {
                listaFinal.Add(new RotacionInventarioDto
                {
                    IdAlmacen = listaData.ToList()[i].AlmId,
                    Almacen = listaData.ToList()[i].AlmNombre,
                    Articulos = this.getArticulosRotacion(listaData, i)
                });
            }

            return listaFinal;
        }
        private List<RotInvArticuloDTo> getArticulosRotacion(IQueryable<Almacen> lista, int index)
        {
            var listaNueva = new List<RotInvArticuloDTo>();

            var listaArticulo = lista.ToList()[index].ArticuloAlmacen.Where(x => x.ArtAlmEstado == (int)EstadoArticuloAlmacen.Activo);
            var listaProyecto = lista.ToList()[index].ProyectoAlmacen.Where(x => x.ProAlmEstado == (int)EstadoProyectoAlmacen.Activo);

            for (var i = 0; i < listaArticulo.Count(); i++)
            {
                listaNueva.Add(new RotInvArticuloDTo
                {
                    IdArticulo = listaArticulo.ToList()[i].ArtAlmArtId,
                    NombreArticulo = context.Articulo.Where(x => x.ArtId == listaArticulo.ToList()[i].ArtAlmArtId).FirstOrDefault().ArtNombre,
                    UnidadStock = listaArticulo.ToList()[i].ArtAlmCantidad,
                    UnidadSalida = this.getUnidadSalida(listaArticulo, listaProyecto, i),
                    Rotacion = this.getRotacion(listaArticulo.ToList()[i].ArtAlmCantidad, this.getUnidadSalida(listaArticulo, listaProyecto, i))
                }
                );
            }

            return listaNueva;

        }

        private int getUnidadSalida(IEnumerable<ArticuloAlmacen> articuloAlmacenes, IEnumerable<ProyectoAlmacen> proyectoAlmacenes, int index)
        {
            var unidadSalida = 0;
            var articuloAlmacen = articuloAlmacenes.ToList()[index];
            var salida = this.getSalidaByProyecto(proyectoAlmacenes, articuloAlmacen);
            var conteoProyectoAlmacenes = proyectoAlmacenes.Count();

            if (conteoProyectoAlmacenes > 0 && salida > 0)
            {
                unidadSalida = salida;
            }
            else
            {
                unidadSalida = salida == 0 ? articuloAlmacen.ArtAlmCantidad : salida;
            }
            return unidadSalida;
        }

        private int getSalidaByProyecto(IEnumerable<ProyectoAlmacen> proyectoAlmacenes, ArticuloAlmacen articuloAlmacen)
        {
            var salida = 0;
            var lista = proyectoAlmacenes.Where(x => x.ProAlmEstado == (int)EstadoProyectoAlmacen.Activo &&
                                                x.ProAlmArtId == articuloAlmacen.ArtAlmArtId &&
                                                x.ProAlmAlmId == articuloAlmacen.ArtAlmAlmId &&
                                                x.ProAlmPro.ProEstado == (int)EstadoProyecto.Vigente);
            for (var i = 0; i < lista.Count(); i++)
            {
                salida += lista.ToList()[i].ProAlmCantidad;
            }
            return salida;
        }

        private string getRotacion(int unidadStock, int unidadSalida)
        {
            double calc = ((double)unidadSalida / (double)unidadStock);
            string tpi = String.Format("{0:0.##}", calc);
            return tpi;
        }

    }
}
