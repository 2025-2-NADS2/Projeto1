using Alma.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alma.Application.Interfaces.Repositorios
{
    // Interface unificada; remova versões antigas que usam CampanhaDoação/Guid.
    public interface ICampanhaRepository
    {
        Task UpdateCampanha(Campanha campanha);
        Task PostCampanha(Campanha campanha);
        Task<List<Campanha>> GetAllCampanhasDisponiveis();
        Task DeleteCampanha(Campanha campanha);
        Task<Campanha?> GetCampanhaByIdAsync(int id);
    }
}
