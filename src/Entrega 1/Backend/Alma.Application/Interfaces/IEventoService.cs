using Alma.Application.DTOs.Evento;
using Alma.Domain.Entities;

namespace Alma.Application.Interfaces.Repositorios
{
    public interface IEventoService
    {
        Task<List<Evento>> GetTodosEventosDisponiveis();
        Task<Guid> CriarNovoEvento(NovoEventoDto dto);
        Task UpdateEvento(NovoEventoDto dto);

    }
}
