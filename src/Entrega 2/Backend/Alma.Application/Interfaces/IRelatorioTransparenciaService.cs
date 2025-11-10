using Alma.Domain.Entities;
using Microsoft.AspNetCore.Http;

public interface IRelatorioTransparenciaService
{
    Task<IEnumerable<RelatorioTransparencia>> ListarRelatorios();
    Task<RelatorioTransparencia> EnviarRelatorio(IFormFile arquivo, string titulo, string descricao);
    Task<(string CaminhoArquivo, string NomeArquivo)> ObterRelatorioParaDownload(Guid id);
}
