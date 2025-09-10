using Alma.Application.Interfaces.Repositorios;
using Alma.Domain.DTOs.Usuario;
using Alma.Domain.Entities;

namespace Alma.Application.Services
{
    public class EventoService : IEventoService
    {
        private readonly IEventoRepository _eventoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EventoService(IEventoRepository eventoRepository, IUnitOfWork unitOfWork)
        {
            _eventoRepository = eventoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Evento>> GetTodosEventosDisponiveis()
        {
            return await _eventoRepository.GetAllEventosDisponiveis();
        }

    }
}
