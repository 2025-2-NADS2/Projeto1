using Alma.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace Alma.Infra.Data
{
    public class AlmaDbContext : DbContext
    {
        public AlmaDbContext(DbContextOptions<AlmaDbContext> options) : base(options) { }

        // Adicione seus DbSets
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurações de entidades se necessário
        }
    }
}