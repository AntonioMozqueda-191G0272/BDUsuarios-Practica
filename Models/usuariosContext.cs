using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BDUsuarios_Practica.Models
{
    public partial class usuariosContext : DbContext
    {
        public usuariosContext()
        {
        }

        public usuariosContext(DbContextOptions<usuariosContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bitácora> Bitácoras { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;user=root;password=GuzmanMA/EVIL1#2115;database=usuarios", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.23-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_0900_ai_ci");

            modelBuilder.Entity<Bitácora>(entity =>
            {
                entity.ToTable("bitácora");

                entity.HasIndex(e => e.UsuarioId, "ID_idx");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.Fecha)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Bitácoras)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ID");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuario");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Contraseña)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.EMail)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("eMail");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.Telefono).HasMaxLength(45);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
