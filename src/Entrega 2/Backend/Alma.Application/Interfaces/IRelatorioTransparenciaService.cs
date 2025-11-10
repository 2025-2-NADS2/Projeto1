using System.Collections.Generic;
using System.Threading.Tasks;
using Alma.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Alma.Application.Interfaces
{
    public interface IRelatorioTransparenciaService
    {
        Task<IEnumerable<RelatorioTransparencia>> ListarRelatorios();
        Task<RelatorioTransparencia> EnviarRelatorio(IFormFile arquivo, string titulo, string descricao);
        Task<(string CaminhoArquivo, string NomeArquivo)> ObterRelatorioParaDownload(int id);
    }
}
