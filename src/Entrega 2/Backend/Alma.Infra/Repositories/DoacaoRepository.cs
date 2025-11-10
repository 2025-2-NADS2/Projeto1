using Alma.Application.Interfaces.Repositorios;
using Alma.Domain.Entities;
using Alma.Infra.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alma.Infra.Repositories
{
    public class DoacaoRepository : IDoacaoRepository
    {
        private readonly AlmaDbContext _context;

        public DoacaoRepository(AlmaDbContext context) => _context = context;

        public async Task<Doacao?> ObterPorId(int id)
            => await _context.Doacoes.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        public async Task<IEnumerable<Doacao>> ObterPorCampanha(int campanhaId)
            => await _context.Doacoes.AsNoTracking()
                .Where(d => d.CampanhaId == campanhaId)
                .ToListAsync();

        public async Task Adicionar(Doacao doacao)
            => await _context.Doacoes.AddAsync(doacao); // commit via UnitOfWork

        public Task Atualizar(Doacao doacao)
        {
            _context.Doacoes.Update(doacao);
            return Task.CompletedTask;
        }
    }
}
