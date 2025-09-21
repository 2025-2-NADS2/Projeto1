using Alma.Application.Interfaces.Repositorios;
using Alma.Domain.Entities;
using Alma.Infra.Data;

namespace Alma.Infra.Repositories
{
    public class InscricoesRepository : BaseRepository<Inscricoes>, IInscricoesRepository
    {
        private readonly AlmaDbContext _context;
        public InscricoesRepository(AlmaDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task PostInscricao(Inscricoes inscricao)
        {
            await _context.Inscricoes.AddAsync(inscricao);
        }
    }
}

