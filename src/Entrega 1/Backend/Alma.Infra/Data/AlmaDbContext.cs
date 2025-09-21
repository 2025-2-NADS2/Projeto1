using Microsoft.EntityFrameworkCore;
using Alma.Domain.Entities;
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

            // Mapeia entidades para tabelas corretas
            modelBuilder.Entity<Usuario>().ToTable("usuarios");
            modelBuilder.Entity<Campanha>().ToTable("campanhas_doacoes");
            modelBuilder.Entity<Evento>().ToTable("eventos");
            modelBuilder.Entity<Historias>().ToTable("historias_destaque");
            modelBuilder.Entity<Inscricoes>().ToTable("inscricoes_eventos");
        }
    }
}