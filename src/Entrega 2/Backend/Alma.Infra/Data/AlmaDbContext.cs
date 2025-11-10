using Alma.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Alma.Infra.Data
{
    public class AlmaDbContext : DbContext
    {
        public AlmaDbContext(DbContextOptions<AlmaDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Campanha> Campanhas { get; set; }
        public DbSet<Doacao> Doacoes { get; set; }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Inscricoes> Inscricoes { get; set; }
        public DbSet<Historias> Historias { get; set; }
        public DbSet<RelatorioTransparencia> RelatoriosTransparencia { get; set; }
        public DbSet<Ouvidoria> OuvidoriaMensagens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Exemplo: converter enum diretamente (se decidir remover backing fields)
            // modelBuilder.Entity<Campanha>()
            //     .Property(c => c.Status)
            //     .HasConversion<string>()
            //     .HasColumnName("status")
            //     .HasMaxLength(15);
        }
    }
}