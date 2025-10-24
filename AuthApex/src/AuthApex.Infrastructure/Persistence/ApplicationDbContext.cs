using AuthApex.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthApex.Infrastructure.Persistence
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
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

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
