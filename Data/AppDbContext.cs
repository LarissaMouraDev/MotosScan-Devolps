using Microsoft.EntityFrameworkCore;
using MotosScan.Models;

namespace MotosScan.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Moto> Motos { get; set; }
        public DbSet<Motorista> Motoristas { get; set; }
        public DbSet<Manutencao> Manutencoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração da entidade Moto
            modelBuilder.Entity<Moto>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Placa).IsUnique();
                entity.Property(e => e.Modelo).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Placa).IsRequired().HasMaxLength(10);
                entity.Property(e => e.Estado).HasMaxLength(20);
                entity.Property(e => e.Localizacao).HasMaxLength(100);
                entity.Property(e => e.ImagemUrl).HasMaxLength(500);
            });

            // Configuração da entidade Motorista
            modelBuilder.Entity<Motorista>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.CPF).IsUnique();
                entity.HasIndex(e => e.CNH).IsUnique();
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
                entity.Property(e => e.CPF).IsRequired().HasMaxLength(14);
                entity.Property(e => e.CNH).IsRequired().HasMaxLength(11);
                entity.Property(e => e.CategoriaCNH).HasMaxLength(20);
                entity.Property(e => e.Telefone).HasMaxLength(15);
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.Status).HasMaxLength(20);

                // Relacionamento com Moto (opcional)
                entity.HasOne(m => m.MotoAtual)
                      .WithMany(moto => moto.Motoristas)
                      .HasForeignKey(m => m.MotoAtualId)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            // Configuração da entidade Manutencao
            modelBuilder.Entity<Manutencao>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.TipoManutencao).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Descricao).IsRequired().HasMaxLength(500);
                entity.Property(e => e.Status).HasMaxLength(20);
                entity.Property(e => e.Oficina).HasMaxLength(100);
                entity.Property(e => e.Observacoes).HasMaxLength(1000);
                entity.Property(e => e.Custo).HasColumnType("decimal(18,2)");

                // Relacionamento com Moto (obrigatório)
                entity.HasOne(m => m.Moto)
                      .WithMany(moto => moto.Manutencoes)
                      .HasForeignKey(m => m.MotoId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Relacionamento com Motorista (opcional)
                entity.HasOne(m => m.Motorista)
                      .WithMany(motorista => motorista.Manutencoes)
                      .HasForeignKey(m => m.MotoristaId)
                      .OnDelete(DeleteBehavior.SetNull);
            });
        }
    }
}