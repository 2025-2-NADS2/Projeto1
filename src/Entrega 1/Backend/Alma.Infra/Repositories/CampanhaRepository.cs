using Alma.Application.Interfaces.Repositorios;
using Alma.Domain.Entities;
using Alma.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Alma.Infra.Repositories
{
    public class CampanhaRepository : BaseRepository<Campanha>, ICampanhaRepository
    {
        private readonly AlmaDbContext _context;
        public CampanhaRepository(AlmaDbContext context) : base(context) 
        {
            _context = context;

        }

        public async Task<List<Campanha>> GetAllCampanhasDisponiveis()
        {
            var now = DateTime.UtcNow; // usa UTC para consistência
            return _context.Campanhas
                .AsNoTracking()
                .Where(e => e.StatusCampanha != Domain.Enum.StatusCampanha.FINALIZADA)
                .OrderBy(e => e.DateCreated)
                .ToList();
        }

        public async Task PostCampanha(Campanha campanha)
        {
            _context.Campanhas.AddAsync(campanha);
        }

        public async Task UpdateCampanha(Campanha campanha)
        {
            _context.Campanhas.Update(campanha);
        }

        public async Task DeleteCampanha(Campanha campanha)
        {
            _context.Campanhas.Remove(campanha);
        }
        public async Task<Campanha> GetCampanhaById(Guid id)
        {
            return _context.Campanhas
                .FirstOrDefault(e => e.Id == id);
        }
    }
}
