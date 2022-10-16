using control_inventario_repository_inventario.Entity;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace control_inventario_repository_inventario.Context
{
    public partial class ControlInventarioContext : DbContext
    {
        public ControlInventarioContext()
        {

        }

        public ControlInventarioContext(DbContextOptions<ControlInventarioContext> options) : base(options)
        {

        }

        public virtual DbSet<Almacen> Almacen { get; set; }
        public virtual DbSet<Articulo> Articulo { get; set; }
        public virtual DbSet<ArticuloAlmacen> ArticuloAlmacen { get; set; }
        public virtual DbSet<ProyectoAlmacen> ProyectoAlmacen { get; set; }
        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Proyecto> Proyecto { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost\\MSSQLSERVER01;Database=bd_mesa_partes_virtual;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Almacen>(entity =>
            {
                entity.HasKey(e => e.AlmId)
                        .HasName("PK__almacen__id");

                entity.ToTable("almacen", "inventario");

                entity.Property(e => e.AlmNombre)
                    .HasColumnName("alm__nombre")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.AlmDireccion)
                    .HasColumnName("alm__direccion")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.AlmEstado)
                    .HasColumnName("alm__estado")
                    .IsUnicode(false);

                entity.Property(e => e.AlmEstado)
                    .HasColumnName("alm__estado")
                    .IsUnicode(false);

                entity.Property(e => e.AlmFechaCreacion)
                    .HasColumnName("alm__fecha_creacion")
                    .IsUnicode(false);

                entity.Property(e => e.AlmFechaActualizacion)
                    .HasColumnName("alm__fecha_actualizacion")
                    .IsUnicode(false);

            });

            modelBuilder.Entity<Articulo>(entity =>
            {
                entity.HasKey(e => e.ArtId)
                        .HasName("PK__articulo__id");

                entity.ToTable("articulo", "inventario");

                entity.Property(e => e.ArtNombre)
                    .HasColumnName("art__nombre")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ArtUrl)
                    .HasColumnName("art__url")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ArtPrecio)
                    .HasColumnName("art__precio")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ArtEstado)
                    .HasColumnName("art__estado")
                    .IsUnicode(false);

                entity.Property(e => e.ArtFechaCreacion)
                    .HasColumnName("art__fecha_creacion")
                    .IsUnicode(false);

                entity.Property(e => e.ArtFechaActualizacion)
                    .HasColumnName("art__fecha_actualizacion")
                    .IsUnicode(false); 

                entity.Property(e => e.ArtCatId)
                    .HasColumnName("art__cat_id")
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ArticuloAlmacen>(entity =>
            {
                entity.HasKey(e => e.ArtAlmId)
                        .HasName("art_alm__id");

                entity.ToTable("articulo_almacen", "inventario");

                entity.Property(e => e.ArtAlmCantidad)
                    .HasColumnName("art_alm__cantidad")
                    .IsUnicode(false);

                entity.Property(e => e.ArtAlmEstado)
                    .HasColumnName("art_alm__estado")
                    .IsUnicode(false);

                entity.HasOne(d => d.ArtAlmArt)
                    .WithMany(p => p.ArticuloAlmacen)
                    .HasForeignKey(d => d.ArtAlmArtId)
                    .HasConstraintName("fk_art_alm__art_id");

                entity.HasOne(d => d.ArtAlmAlm)
                    .WithMany(p => p.ArticuloAlmacen)
                    .HasForeignKey(d => d.ArtAlmAlmId)
                    .HasConstraintName("fk_art_alm__alm_id");
            });

            modelBuilder.Entity<ProyectoAlmacen>(entity =>
            {
                entity.HasKey(e => e.ProAlmId)
                        .HasName("pro_alm__id");

                entity.ToTable("proyecto_almacen", "inventario");

                entity.Property(e => e.ProAlmCantidad)
                    .HasColumnName("pro_alm__cantidad")
                    .IsUnicode(false);

                entity.Property(e => e.ProAlmArtId)
                    .HasColumnName("pro_alm__art_id")
                    .IsUnicode(false);

                entity.Property(e => e.ProAlmEstado)
                    .HasColumnName("pro_alm__estado")
                    .IsUnicode(false);

                entity.HasOne(d => d.ProAlmAlm)
                    .WithMany(p => p.ProyectoAlmacen)
                    .HasForeignKey(d => d.ProAlmAlmId)
                    .HasConstraintName("fk_pro_alm__alm_id");

                entity.HasOne(d => d.ProAlmPro)
                    .WithMany(p => p.ProyectoAlmacen)
                    .HasForeignKey(d => d.ProAlmProId)
                    .HasConstraintName("fk_pro_alm__pro_id");
            });

            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.CatId)
                        .HasName("PK__cat__id");

                entity.ToTable("categoria", "inventario");

                entity.Property(e => e.CatNombre)
                    .HasColumnName("cat__nombre")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Proyecto>(entity =>
            {
                entity.HasKey(e => e.ProId)
                        .HasName("PK__pro__id");

                entity.ToTable("proyecto", "inventario");

                entity.Property(e => e.ProNombre)
                    .HasColumnName("pro__nombre")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ProCliente)
                    .HasColumnName("pro__cliente")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ProFechaInicio)
                    .HasColumnName("pro__fecha_inicio")
                    .IsUnicode(false);

                entity.Property(e => e.ProFechaFin)
                    .HasColumnName("pro__fecha_fin")
                    .IsUnicode(false);

                entity.Property(e => e.ProEstado)
                    .HasColumnName("pro__estado")
                    .IsUnicode(false);

                entity.Property(e => e.ProFechaCreacion)
                    .HasColumnName("pro__fecha_creacion")
                    .IsUnicode(false);

                entity.Property(e => e.ProFechaActualizacion)
                    .HasColumnName("pro__fecha_actualizacion")
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
