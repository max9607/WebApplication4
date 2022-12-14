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
        public virtual DbSet<TbCategorium> TbCategoria { get; set; } = null!;
        public virtual DbSet<TbDerivado> TbDerivados { get; set; } = null!;
        public virtual DbSet<TbEmpresa> TbEmpresas { get; set; } = null!;
        public virtual DbSet<TbEstadoTicket> TbEstadoTickets { get; set; } = null!;
        public virtual DbSet<TbFechaTicket> TbFechaTickets { get; set; } = null!;
        public virtual DbSet<TbPermiso> TbPermisos { get; set; } = null!;
        public virtual DbSet<TbPrioridadTicket> TbPrioridadTickets { get; set; } = null!;
        public virtual DbSet<TbTicket> TbTickets { get; set; } = null!;
        public virtual DbSet<TbTicketsCerrado> TbTicketsCerrados { get; set; } = null!;
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
                    .HasName("PK__tb_Acces__99B2858FA626FFE9");

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

            modelBuilder.Entity<TbCategorium>(entity =>
            {
                entity.HasKey(e => e.IdProblema)
                    .HasName("PK__tb_Categ__0FDB5F4AA41314AA");

                entity.ToTable("tb_Categoria");

                entity.Property(e => e.Problema)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TbDerivado>(entity =>
            {
                entity.HasKey(e => e.IdDerivado)
                    .HasName("PK__tb_Deriv__91186DFA631C9159");

                entity.ToTable("tb_Derivado");

                entity.HasOne(d => d.IdTicketNavigation)
                    .WithMany(p => p.TbDerivados)
                    .HasForeignKey(d => d.IdTicket)
                    .HasConstraintName("FK__tb_Deriva__IdTic__4AB81AF0");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.TbDerivados)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__tb_Deriva__IdUsu__49C3F6B7");
            });

            modelBuilder.Entity<TbEmpresa>(entity =>
            {
                entity.HasKey(e => e.IdEmpresa)
                    .HasName("PK__tb_Empre__5EF4033E096EDF8A");

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

            modelBuilder.Entity<TbEstadoTicket>(entity =>
            {
                entity.HasKey(e => e.IdEstado)
                    .HasName("PK__tb_Estad__FBB0EDC1D2963AAB");

                entity.ToTable("tb_EstadoTicket");

                entity.Property(e => e.EstadoTicket)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TbFechaTicket>(entity =>
            {
                entity.HasKey(e => e.IdFecha)
                    .HasName("PK__tb_Fecha__8D0F205AC58090FF");

                entity.ToTable("tb_FechaTicket");

                entity.Property(e => e.FechaCerrado).HasColumnType("datetime");

                entity.Property(e => e.FechaCreado).HasColumnType("datetime");
            });

            modelBuilder.Entity<TbPermiso>(entity =>
            {
                entity.HasKey(e => e.IdPermiso)
                    .HasName("PK__tb_Permi__0D626EC8659BB50A");

                entity.ToTable("tb_Permiso");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TbPrioridadTicket>(entity =>
            {
                entity.HasKey(e => e.IdPrioridad)
                    .HasName("PK__tb_Prior__0FC70DD5A383A030");

                entity.ToTable("tb_PrioridadTicket");

                entity.Property(e => e.Prioridad)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TbTicket>(entity =>
            {
                entity.HasKey(e => e.IdTicket)
                    .HasName("PK__tb_Ticke__4B93C7E79D9D8719");

                entity.ToTable("tb_Ticket");

                entity.Property(e => e.DescripionDetallada)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.DespricionP)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Localizacion)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.TbTickets)
                    .HasForeignKey(d => d.IdEstado)
                    .HasConstraintName("FK__tb_Ticket__IdEst__38996AB5");

                entity.HasOne(d => d.IdFechaNavigation)
                    .WithMany(p => p.TbTickets)
                    .HasForeignKey(d => d.IdFecha)
                    .HasConstraintName("FK__tb_Ticket__IdFec__398D8EEE");

                entity.HasOne(d => d.IdPrioridadNavigation)
                    .WithMany(p => p.TbTickets)
                    .HasForeignKey(d => d.IdPrioridad)
                    .HasConstraintName("FK__tb_Ticket__IdPri__37A5467C");

                entity.HasOne(d => d.IdProblemaNavigation)
                    .WithMany(p => p.TbTickets)
                    .HasForeignKey(d => d.IdProblema)
                    .HasConstraintName("FK__tb_Ticket__IdPro__3A81B327");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.TbTickets)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__tb_Ticket__IdUsu__36B12243");
            });

            modelBuilder.Entity<TbTicketsCerrado>(entity =>
            {
                entity.HasKey(e => e.IdCerrados)
                    .HasName("PK__tb_Ticke__6EA883BB786761BA");

                entity.ToTable("tb_TicketsCerrados");

                entity.Property(e => e.Cliente)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Comentario)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.DespricionP)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FechaCerrado).HasColumnType("datetime");

                entity.Property(e => e.FechaCreado).HasColumnType("datetime");

                entity.Property(e => e.Receptor)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TbUsuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK__tb_Usuar__5B65BF97907B1255");

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
