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

        public async Task<int> InscreverEvento(int eventoId, string usuarioId)
        {
            if (eventoId <= 0) throw new ArgumentException("Evento inválido.");
            if (string.IsNullOrWhiteSpace(usuarioId)) throw new ArgumentException("Usuário inválido.");

            // evita duplicidade
            if (await _inscricoesRepository.ExisteInscricao(usuarioId, eventoId))
                throw new InvalidOperationException("Usuário já inscrito neste evento.");

            var inscricao = new Inscricoes
            {
                UsuarioId = usuarioId,
                EventoId = eventoId,
                DataInscricao = DateTime.UtcNow
            };

            await _inscricoesRepository.PostInscricao(inscricao);
            await _unitOfWork.CommitAsync();
            
            return inscricao.Id;
        }
    }
}
