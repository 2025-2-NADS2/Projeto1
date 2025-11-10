using Alma.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alma.Application.Interfaces.Repositorios
{
    public interface IDoacaoRepository
    {
        Task<Doacao?> ObterPorId(int id);
        Task<IEnumerable<Doacao>> ObterPorCampanha(int campanhaId);
        Task Adicionar(Doacao doacao);
        Task Atualizar(Doacao doacao);
    }
}
