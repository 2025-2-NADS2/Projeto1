using Alma.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

[ApiController]
[Route("api/[controller]")]
public class DoacoesController : ControllerBase
{
    private readonly DoacaoService _doacaoService;

    public DoacoesController(DoacaoService doacaoService)
    {
        _doacaoService = doacaoService;
    }

    [HttpPost]
    public async Task<IActionResult> CriarDoacao([FromBody] DoacaoDto request)
    {
        var urlPagamento = await _doacaoService.CriarSessaoPagamentoAsync(request.CampanhaId, request.Valor);
        return Ok(new { CheckoutUrl = urlPagamento });
    }

    [HttpPost("webhook")]
    public async Task<IActionResult> Webhook([FromBody] JsonElement payload)
    {
        try
        {
            var externalReference = payload.GetProperty("data").GetProperty("id").GetString();
            if (Guid.TryParse(externalReference, out var doacaoId))
            {
                await _doacaoService.ConfirmarDoacaoAsync(doacaoId);
            }
            return Ok();
        }
        catch
        {
            return BadRequest();
        }
    }
}
