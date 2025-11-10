using Alma.Application.Interfaces.Repositorios;
using Alma.Domain.Entities;
using Alma.Domain.Enum;
using Alma.Infra.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            return await _context.Campanhas
                .AsNoTracking()
                .Where(e => e.Status != StatusCampanha.Finalizada)
                .OrderBy(e => e.CriadoEm)
                .ToListAsync();
        }

        public async Task<Campanha?> GetCampanhaByIdAsync(int id)
        {
            return await _context.Campanhas
                .Include(c => c.Doacoes)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task PostCampanha(Campanha campanha)
        {
            await _context.Campanhas.AddAsync(campanha);
        }

        public Task UpdateCampanha(Campanha campanha)
        {
            _context.Campanhas.Update(campanha);
            return Task.CompletedTask;
        }

        public Task DeleteCampanha(Campanha campanha)
        {
            _context.Campanhas.Remove(campanha);
            return Task.CompletedTask;
        }
    }
}
