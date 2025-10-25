using AuthApex.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthApex.Infrastructure.Persistence
{
    public class AuthDbContext(DbContextOptions<AuthDbContext> options) : DbContext(options)
    {
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(builder =>
            {
                builder.OwnsOne(u => u.Email, email =>
                {
                    email.Property(e => e.Endereco)
                         .HasColumnName("Email")
                         .IsRequired();
                });
            });

            modelBuilder.Entity<Usuario>(builder =>
            {
                builder.OwnsOne(u => u.SenhaHash, senha =>
                {
                    senha.Property(e => e.ValorHash)
                         .HasColumnName("SenhaHash")
                         .IsRequired();
                });
            });

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuthDbContext).Assembly);
        }
    }
}
