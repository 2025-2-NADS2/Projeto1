using System.Collections.Generic;
using System.Threading.Tasks;
using Alma.Domain.Entities;

namespace Alma.Application.Interfaces.Repositorios
{
    public interface IRelatorioTransparenciaRepository
    {
        Task Adicionar(RelatorioTransparencia relatorio);
        Task<IEnumerable<RelatorioTransparencia>> List();
        Task<RelatorioTransparencia?> GetById(int id);
    }
}