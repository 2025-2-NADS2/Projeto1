using Alma.Application.DTOs;
using Alma.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Threading.Tasks;

namespace Alma.API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoacoesController : ControllerBase
    {
        private readonly IDoacaoService _doacaoService;

        public DoacoesController(IDoacaoService doacaoService)
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
                if (int.TryParse(externalReference, out var doacaoId))
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
}
