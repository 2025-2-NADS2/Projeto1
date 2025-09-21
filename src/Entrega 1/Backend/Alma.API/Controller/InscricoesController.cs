using Alma.Application.Interfaces.Repositorios;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Alma.API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class InscricoesController : ControllerBase
    {
        private readonly IInscricoesService _inscricoesService;
        public InscricoesController(IInscricoesService inscricoesService)
        {
            _inscricoesService = inscricoesService;
        }

        [HttpPost("inscrever/{eventoId:guid}")]
        public async Task<IActionResult> InscreverEvento(Guid eventoId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized("Usuário não autenticado");

            var id = await _inscricoesService.InscreverEvento(eventoId, Guid.Parse(userId));
            return Ok(id);
        }
    }
}

