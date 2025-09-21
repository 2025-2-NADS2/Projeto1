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

            // Mapeia a entidade Usuario para a tabela 'usuarios' no schema 'railway'
            modelBuilder.Entity<Usuario>().ToTable("usuarios", "railway");

            modelBuilder.Entity<Usuario>()
                .Property(u => u.DateCreated)
                .HasColumnName("criado_em");
        }
    }
}