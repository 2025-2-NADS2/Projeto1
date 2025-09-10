using Alma.Application.Interfaces.Repositorios;
using Alma.Application.Services;
using Alma.Infra.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Alma.API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class InscricoesController : ControllerBase
    {
        private readonly AlmaDbContext _context;
        private readonly IInscricoesService _inscricoesService;
        public InscricoesController(AlmaDbContext context, IInscricoesService inscricoesService)
        {
            _context = context;
            _inscricoesService = inscricoesService;
        }

        [HttpPost("inscrever/{eventoId}")]
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

