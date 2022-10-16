using control_inventario_repository_personal.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace control_inventario_repository_personal.Context
{
    public partial class ControlInventarioContext : DbContext
    {
        public ControlInventarioContext() { }


        public ControlInventarioContext(DbContextOptions<ControlInventarioContext> options) : base(options)
        {

        }

        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<RolMenu> RolMenu { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

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
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.UsuId)
                        .HasName("PK__usuario__id");

                entity.ToTable("usuario", "personal");

                entity.Property(e => e.UsuNombre)
                    .IsRequired()
                    .HasColumnName("usu__nombre")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UsuApellidos)
                    .IsRequired()
                    .HasColumnName("usu__apellidos")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UsuUsuario)
                    .IsRequired()
                    .HasColumnName("usu__usuario")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UsuContrasenia)
                    .IsRequired()
                    .HasColumnName("usu__contrasenia")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UsuImagenUrl)
                    .HasColumnName("usu__imagen_url")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UsuEstado)
                    .HasColumnName("usu__estado")
                    .IsUnicode(false);

                entity.HasOne(d => d.UsuRol)
                      .WithMany(p => p.Usuario)
                      .HasForeignKey(d => d.UsuRolId)
                      .OnDelete(DeleteBehavior.NoAction)
                      .HasConstraintName("fk_usu__rol_id");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(e => e.RolId)
                    .HasName("PK__rol__id");

                entity.ToTable("rol", "personal");

                entity.Property(e => e.RolNombre)
                    .IsRequired()
                    .HasColumnName("rol__nombre")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RolMenu>(entity =>
            {
                entity.HasKey(e => e.RolMenId)
                    .HasName("PK__rol_menu__id");

                entity.ToTable("rol_menu", "personal");

                entity.HasOne(d => d.RolMenMen)
                    .WithMany(p => p.RolMenu)
                    .HasForeignKey(d => d.RolMenMenId)
                    .HasConstraintName("fk_rol_men__men_id");

                entity.HasOne(d => d.RolMenuRol)
                    .WithMany(p => p.RolMenu)
                    .HasForeignKey(d => d.RolMenRolId)
                    .HasConstraintName("fk_rol_men__rol_id");
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.HasKey(e => e.MenId)
                    .HasName("PK__men__id");

                entity.ToTable("menu", "personal");

                entity.Property(e => e.MenNombre)
                    .IsRequired()
                    .HasColumnName("men__nombre")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.MenPadreId)
                    .HasColumnName("men__padre_id")
                    .IsUnicode(false);

                entity.Property(e => e.MenIconUrl)
                    .HasColumnName("men__icon_url")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.MenLink)
                    .HasColumnName("men__link")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


    }
}
