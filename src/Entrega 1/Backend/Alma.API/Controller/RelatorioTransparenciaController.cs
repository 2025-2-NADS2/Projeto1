using Alma.Application.Interfaces;
using Alma.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class RelatorioTransparenciaController : ControllerBase
{
    private readonly IRelatorioTransparenciaService _transparenciaService;

    public RelatorioTransparenciaController(IRelatorioTransparenciaService transparenciaService)
    {
        _transparenciaService = transparenciaService;
    }

    /// <summary>
    /// Lista todos os relatórios disponíveis
    /// </summary>
    [HttpGet("listar")]
    public async Task<IActionResult> Listar()
    {
        var relatorios = await _transparenciaService.ListarRelatorios();
        return Ok(relatorios);
    }

    /// <summary>
    /// Faz o upload de um arquivo PDF (apenas para administradores)
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpPost("upload")]
    public async Task<IActionResult> Upload([FromForm] IFormFile arquivo, [FromForm] string titulo, [FromForm] string descricao)
    {
        if (arquivo == null || arquivo.Length == 0)
            return BadRequest("Nenhum arquivo enviado.");

        var relatorio = await _transparenciaService.EnviarRelatorio(arquivo, titulo, descricao);
        return Ok(relatorio);
    }

    /// <summary>
    /// Faz o download de um relatório específico
    /// </summary>
    [HttpGet("download/{id}")]
    public async Task<IActionResult> Download(Guid id)
    {
        var resultado = await _transparenciaService.ObterRelatorioParaDownload(id);

        if (resultado.CaminhoArquivo == null)
            return NotFound("Relatório não encontrado.");

        // Usar PhysicalFile para streaming direto do disco, sem carregar na memória
        return PhysicalFile(resultado.CaminhoArquivo, "application/pdf", resultado.NomeArquivo);
    }
}