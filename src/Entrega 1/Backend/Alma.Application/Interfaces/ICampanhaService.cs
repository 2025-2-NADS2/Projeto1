using Alma.Application.DTOs.Campanha;
using Alma.Domain.Entities;

namespace Alma.Application.Interfaces.Repositorios
{
    public interface ICampanhaService
    {
        Task<List<Campanha>> GetTodasCampanhasDisponiveis();
        Task<Guid> CriarNovaCampanha(NovaCampanhaDto dto);
        Task UpdateCampanha(NovaCampanhaDto dto);
    }
}
