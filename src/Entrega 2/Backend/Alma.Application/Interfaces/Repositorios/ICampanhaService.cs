using Alma.Application.DTOs.Campanha;
using Alma.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alma.Application.Interfaces.Repositorios
{
    public interface ICampanhaService
    {
        Task<List<Campanha>> GetTodasCampanhasDisponiveis();
        Task<int> CriarNovaCampanha(NovaCampanhaDto dto);
        Task UpdateCampanha(NovaCampanhaDto dto);
        Task DeleteCampanha(int id);
    }
}