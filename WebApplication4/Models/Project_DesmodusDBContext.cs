using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApplication4.Models
{
    public partial class Project_DesmodusDBContext : DbContext
    {
        public Project_DesmodusDBContext()
        {
        }

        public Project_DesmodusDBContext(DbContextOptions<Project_DesmodusDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TbAcceso> TbAccesos { get; set; } = null!;
        public virtual DbSet<TbEmpresa> TbEmpresas { get; set; } = null!;
        public virtual DbSet<TbPermiso> TbPermisos { get; set; } = null!;
        public virtual DbSet<TbUsuario> TbUsuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("server=localhost; database=Project_DesmodusDB; integrated security=true; ");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TbAcceso>(entity =>
            {
                entity.HasKey(e => e.IdAcceso)
                    .HasName("PK__tb_Acces__99B2858F4B572477");

                entity.ToTable("tb_Acceso");

                entity.Property(e => e.Clave)
                .HasMaxLength(255)
                .IsUnicode(false);

                entity.Property(e => e.Correo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdPermisoNavigation)
                    .WithMany(p => p.TbAccesos)
                    .HasForeignKey(d => d.IdPermiso)
                    .HasConstraintName("FK__tb_Acceso__IdPer__2C3393D0");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.TbAccesos)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__tb_Acceso__IdUsu__2B3F6F97");
            });

            modelBuilder.Entity<TbEmpresa>(entity =>
            {
                entity.HasKey(e => e.IdEmpresa)
                    .HasName("PK__tb_Empre__5EF4033EA8619F96");

                entity.ToTable("tb_Empresa");

                entity.Property(e => e.Nit)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TbPermiso>(entity =>
            {
                entity.HasKey(e => e.IdPermiso)
                    .HasName("PK__tb_Permi__0D626EC81DB16237");

                entity.ToTable("tb_Permiso");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TbUsuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK__tb_Usuar__5B65BF973DC080F2");

                entity.ToTable("tb_Usuario");

                entity.Property(e => e.Apellido1)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Apellido_1");

                entity.Property(e => e.Apellido2)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Apellido_2");

                entity.Property(e => e.Correo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdEmpresaNavigation)
                    .WithMany(p => p.TbUsuarios)
                    .HasForeignKey(d => d.IdEmpresa)
                    .HasConstraintName("FK__tb_Usuari__IdEmp__286302EC");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
