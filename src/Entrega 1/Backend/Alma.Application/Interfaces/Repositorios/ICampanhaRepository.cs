using Alma.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alma.Application.Interfaces.Repositorios
{
    public interface ICampanhaRepository
    {
        Task UpdateCampanha(Campanha campanha);
        Task PostCampanha(Campanha campanha);
        Task<List<Campanha>> GetAllCampanhasDisponiveis();
        Task DeleteCampanha(Campanha campanha);
        Task<Campanha>GetCampanhaById(Guid id);
    }
}
