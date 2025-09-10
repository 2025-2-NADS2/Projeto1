using Alma.Domain.Entities;

namespace Alma.Application.Interfaces.Repositorios
{
    public interface IEventoService
    {
        Task<List<Evento>> GetTodosEventosDisponiveis();
    }
}
