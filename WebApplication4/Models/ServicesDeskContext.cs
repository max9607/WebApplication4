using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApplication4.Models
{
    public partial class ServicesDeskContext : DbContext
    {
        public ServicesDeskContext()
        {
        }

        public ServicesDeskContext(DbContextOptions<ServicesDeskContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TbAcceso> TbAcceso { get; set; } = null!;
        public virtual DbSet<TbCategoria> TbCategoria { get; set; } = null!;
        public virtual DbSet<TbComentario> TbComentario { get; set; } = null!;
        public virtual DbSet<TbDerivado> TbDerivado { get; set; } = null!;
        public virtual DbSet<TbEmpresa> TbEmpresa { get; set; } = null!;
        public virtual DbSet<TbEstadoTicket> TbEstadoTicket { get; set; } = null!;
        public virtual DbSet<TbFechaTicket> TbFechaTicket { get; set; } = null!;
        public virtual DbSet<TbPermiso> TbPermiso { get; set; } = null!;
        public virtual DbSet<TbPrioridadTicket> TbPrioridadTicket { get; set; } = null!;
        public virtual DbSet<TbTicket> TbTicket { get; set; } = null!;
        public virtual DbSet<TbTicketsCerrados> TbTicketsCerrados { get; set; } = null!;
        public virtual DbSet<TbUsuario> TbUsuario { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost ;Database=ServicesDesk; User=sistema; Password=sczz; Trust Server Certificate=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TbAcceso>(entity =>
            {
                entity.HasKey(e => e.IdAcceso)
                    .HasName("PK__tb_Acces__99B2858F68227C83");

                entity.ToTable("tb_Acceso");

                entity.Property(e => e.Clave)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Correo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Estado).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdPermisoNavigation)
                    .WithMany(p => p.TbAcceso)
                    .HasForeignKey(d => d.IdPermiso)
                    .HasConstraintName("FK__tb_Acceso__IdPer__5165187F");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.TbAcceso)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__tb_Acceso__IdUsu__5070F446");
            });

            modelBuilder.Entity<TbCategoria>(entity =>
            {
                entity.HasKey(e => e.IdProblema)
                    .HasName("PK__tb_Categ__0FDB5F4A50AA7EEB");

                entity.ToTable("tb_Categoria");

                entity.Property(e => e.Problema)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TbComentario>(entity =>
            {
                entity.HasKey(e => e.IdComentario)
                    .HasName("PK__tb_Comen__DDBEFBF9E3DECD55");

                entity.ToTable("tb_Comentario");

                entity.Property(e => e.Comentario)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdTicketNavigation)
                    .WithMany(p => p.TbComentario)
                    .HasForeignKey(d => d.IdTicket)
                    .HasConstraintName("FK__tb_Coment__IdTic__6754599E");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.TbComentario)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__tb_Coment__IdUsu__66603565");
            });

            modelBuilder.Entity<TbDerivado>(entity =>
            {
                entity.HasKey(e => e.IdDerivado)
                    .HasName("PK__tb_Deriv__91186DFA8CF99B42");

                entity.ToTable("tb_Derivado");

                entity.HasOne(d => d.IdTicketNavigation)
                    .WithMany(p => p.TbDerivado)
                    .HasForeignKey(d => d.IdTicket)
                    .HasConstraintName("FK__tb_Deriva__IdTic__6383C8BA");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.TbDerivado)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__tb_Deriva__IdUsu__628FA481");
            });

            modelBuilder.Entity<TbEmpresa>(entity =>
            {
                entity.HasKey(e => e.IdEmpresa)
                    .HasName("PK__tb_Empre__5EF4033EC4AFC3BB");

                entity.ToTable("tb_Empresa");

                entity.Property(e => e.Estado).HasDefaultValueSql("((1))");

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
                    .HasName("PK__tb_Estad__FBB0EDC189A8F2F1");

                entity.ToTable("tb_EstadoTicket");

                entity.Property(e => e.EstadoTicket)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TbFechaTicket>(entity =>
            {
                entity.HasKey(e => e.IdFecha)
                    .HasName("PK__tb_Fecha__8D0F205AEB36FE9B");

                entity.ToTable("tb_FechaTicket");

                entity.Property(e => e.FechaCerrado).HasColumnType("datetime");

                entity.Property(e => e.FechaCreado).HasColumnType("datetime");
            });

            modelBuilder.Entity<TbPermiso>(entity =>
            {
                entity.HasKey(e => e.IdPermiso)
                    .HasName("PK__tb_Permi__0D626EC8720E7F0D");

                entity.ToTable("tb_Permiso");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TbPrioridadTicket>(entity =>
            {
                entity.HasKey(e => e.IdPrioridad)
                    .HasName("PK__tb_Prior__0FC70DD555838655");

                entity.ToTable("tb_PrioridadTicket");

                entity.Property(e => e.Prioridad)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TiempoRespuesta).HasColumnName("Tiempo_Respuesta");
            });

            modelBuilder.Entity<TbTicket>(entity =>
            {
                entity.HasKey(e => e.IdTicket)
                    .HasName("PK__tb_Ticke__4B93C7E7171DE8C4");

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
                    .WithMany(p => p.TbTicket)
                    .HasForeignKey(d => d.IdEstado)
                    .HasConstraintName("FK__tb_Ticket__IdEst__5DCAEF64");

                entity.HasOne(d => d.IdFechaNavigation)
                    .WithMany(p => p.TbTicket)
                    .HasForeignKey(d => d.IdFecha)
                    .HasConstraintName("FK__tb_Ticket__IdFec__5EBF139D");

                entity.HasOne(d => d.IdPrioridadNavigation)
                    .WithMany(p => p.TbTicket)
                    .HasForeignKey(d => d.IdPrioridad)
                    .HasConstraintName("FK__tb_Ticket__IdPri__5CD6CB2B");

                entity.HasOne(d => d.IdProblemaNavigation)
                    .WithMany(p => p.TbTicket)
                    .HasForeignKey(d => d.IdProblema)
                    .HasConstraintName("FK__tb_Ticket__IdPro__5FB337D6");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.TbTicket)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__tb_Ticket__IdUsu__5BE2A6F2");
            });

            modelBuilder.Entity<TbTicketsCerrados>(entity =>
            {
                entity.HasKey(e => e.IdCerrados)
                    .HasName("PK__tb_Ticke__6EA883BB587D7DFA");

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
                    .HasName("PK__tb_Usuar__5B65BF975F97C0E0");

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

                entity.Property(e => e.Estado).HasDefaultValueSql("((1))");

                entity.Property(e => e.FechaNacimiento)
                    .HasColumnType("date")
                    .HasColumnName("fecha_nacimiento");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdEmpresaNavigation)
                    .WithMany(p => p.TbUsuario)
                    .HasForeignKey(d => d.IdEmpresa)
                    .HasConstraintName("FK__tb_Usuari__IdEmp__4D94879B");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
