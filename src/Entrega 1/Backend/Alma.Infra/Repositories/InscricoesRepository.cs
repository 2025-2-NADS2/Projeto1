using Alma.Application.Interfaces.Repositorios;
using Alma.Domain.Entities;
using Alma.Infra.Data;
using Microsoft.EntityFrameworkCore;

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

        public async Task<bool> ExisteInscricao(string usuarioId, int eventoId)
        {
            return await _context.Inscricoes.AsNoTracking()
                .AnyAsync(i => i.UsuarioId == usuarioId && i.EventoId == eventoId);
        }
    }
}

