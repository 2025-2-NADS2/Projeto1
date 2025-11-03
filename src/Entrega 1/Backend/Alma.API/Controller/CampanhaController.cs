using Alma.Application.DTOs.Campanha;
using Alma.Application.DTOs.Evento;
using Alma.Application.Interfaces.Repositorios;
using Alma.Domain.Entities;
using Alma.Infra.Data;
using Microsoft.AspNetCore.Mvc;

namespace Alma.API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class CampanhaController : ControllerBase
    {
        private readonly AlmaDbContext _context;
        private readonly ICampanhaService _campanhaService;
        public CampanhaController(AlmaDbContext context, ICampanhaService campanhaService)
        {
            _context = context;
            _campanhaService = campanhaService;
        }

        [HttpGet("get/campanha")]
        public async Task<ActionResult<List<Evento>>> GetTodasCampanhasDisponiveis()
        {
            try
            {
                var eventos = await _campanhaService.GetTodasCampanhasDisponiveis();
                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao buscar campanha: {ex.Message}");
            }
        }

        [HttpPost("post/novo/campanha")]
        public async Task<IActionResult> CriarNovaCampanha([FromBody] NovaCampanhaDto dto)
        {
            try
            {
                await _campanhaService.CriarNovaCampanha(dto);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro interno no servidor.", detalhe = ex.Message });
            }
        }
        
        [HttpPut("put/update/campanha")]
        public async Task<IActionResult> AtualizaCampanha([FromBody] NovaCampanhaDto dto)
        {
            try
            {
                await _campanhaService.UpdateCampanha(dto);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro interno no servidor.", detalhe = ex.Message });
            }
        }

        [HttpPut("delete/campanha/{id: guid}")]
        public async Task<IActionResult> DeleteCampanha(Guid id)
        {
            try
            {
                await _campanhaService.DeleteCampanha(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro interno no servidor.", detalhe = ex.Message });
            }
        }

    }
}
