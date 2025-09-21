using Alma.Application.Interfaces.Repositorios;
using Alma.Domain.Entities;

namespace Alma.Application.Services
{
    public class InscricoesService : IInscricoesService
    {
        private readonly IInscricoesRepository _inscricoesRepository;
        private readonly IUnitOfWork _unitOfWork;

        public InscricoesService(IInscricoesRepository inscricoesRepository, IUnitOfWork unitOfWork)
        {
            _inscricoesRepository = inscricoesRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> InscreverEvento(Guid eventoId, Guid userId)
        {
            var inscricao = new Inscricoes
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                EventoId = eventoId,
                DateCreated = DateTime.UtcNow
            };

            await _inscricoesRepository.PostInscricao(inscricao);
            await _unitOfWork.CommitAsync();
            
            return inscricao.Id;
        }
    }
}
