using Alma.Application.Interfaces.Repositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Alma.API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // exige autenticação por JWT no endpoint de inscrição
    public class InscricoesController : ControllerBase
    {
        private readonly IInscricoesService _inscricoesService;
        public InscricoesController(IInscricoesService inscricoesService)
        {
            _inscricoesService = inscricoesService;
        }

        [HttpPost("inscrever/{eventoId:int}")]
        public async Task<IActionResult> InscreverEvento(int eventoId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(userId))
                return Unauthorized(new { mensagem = "Usuário não autenticado" });

            var id = await _inscricoesService.InscreverEvento(eventoId, userId);
            return CreatedAtAction(nameof(InscreverEvento), new { eventoId, id }, new { id });
        }
    }
}

