using Microsoft.EntityFrameworkCore;
using Alma.Domain.Entities;
using Alma.API.Controller;

namespace Alma.Infra.Data
{
    public class AlmaDbContext : DbContext
    {
        public AlmaDbContext(DbContextOptions<AlmaDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Campanha> Campanhas { get; set; }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Historias> Historias { get; set; }
        public DbSet<Inscricoes> Inscricoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Aqui você pode usar Fluent API para configurações específicas
        }
    }
}