using Microsoft.EntityFrameworkCore;
using TransportApex.Domain.Entities;

namespace TransportApex.Infrastructure.Persistence
{
    public class TransportDbContext(DbContextOptions<TransportDbContext> options) : DbContext(options)
    {
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Entrega> Entregas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Fornecedor>(builder =>
            {
                builder.OwnsOne(u => u.Cnpj, cnpj =>
                {
                    cnpj.Property(e => e.Numero)
                         .HasColumnName("Cnpj")
                         .IsRequired();
                });
            });

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TransportDbContext).Assembly);
        }
    }
}
