using Alma.Application.Interfaces;
using Alma.Application.Interfaces.Repositorios;
using Alma.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

public class TransparenciaService : IRelatorioTransparenciaService
{
    private readonly IRelatorioTransparenciaRepository _repository;
    private readonly IWebHostEnvironment _env;

    public TransparenciaService(IRelatorioTransparenciaRepository repository, IWebHostEnvironment env)
    {
        _repository = repository;
        _env = env;
    }

    public async Task<IEnumerable<RelatorioTransparencia>> ListarRelatorios()
        => await _repository.List();

    public async Task<RelatorioTransparencia> EnviarRelatorio(IFormFile arquivo, string titulo, string descricao)
    {
        var pasta = Path.Combine(_env.WebRootPath ?? "wwwroot", "uploads", "transparencia");
        if (!Directory.Exists(pasta))
            Directory.CreateDirectory(pasta);

        var nomeArquivo = $"{Guid.NewGuid()}_{arquivo.FileName}";
        var caminhoCompleto = Path.Combine(pasta, nomeArquivo);

        using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
        {
            await arquivo.CopyToAsync(stream);
        }

        var relatorio = new RelatorioTransparencia
        {
            Titulo = titulo,
            Descricao = descricao,
            CaminhoArquivo = $"/uploads/transparencia/{nomeArquivo}"
        };

        await _repository.Adicionar(relatorio);
        return relatorio;
    }

    public async Task<(string CaminhoArquivo, string NomeArquivo)> ObterRelatorioParaDownload(Guid id)
    {
        try
        {
            var relatorio = await _repository.GetById(id);
            if (relatorio == null) return (null, null);

            var caminhoRelativo = relatorio.CaminhoArquivo.TrimStart('/', '\\');
            var caminhoCompleto = Path.Combine(_env.WebRootPath ?? "wwwroot", caminhoRelativo);

            if (!File.Exists(caminhoCompleto)) return (null, null);

            return (caminhoCompleto, Path.GetFileName(caminhoCompleto));
        }
        catch (Exception ex)
        {
            // Log o erro (ex.: _logger.LogError(ex, "Erro ao obter relatório"));
            return (null, null);
        }
    }

}
