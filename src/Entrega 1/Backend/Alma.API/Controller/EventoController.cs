using Alma.Application.DTOs.Evento;
using Alma.Application.Interfaces.Repositorios;
using Alma.Domain.Entities;
using Alma.Infra.Data;
using Microsoft.AspNetCore.Mvc;

namespace Alma.API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {
        private readonly IEventoService _eventoService;

        public EventoController(IEventoService eventoService)
        {
            _eventoService = eventoService;
        }

        [HttpGet("get/eventos")] // retorna somente disponíveis
        public async Task<ActionResult<List<Evento>>> GetTodosEventosDisponiveis()
        {
            try
            {
                var eventos = await _eventoService.GetTodosEventosDisponiveis();
                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro interno ao buscar eventos.", detalhe = ex.Message });
            }
        }

        [HttpPost("post/novo/evento")]
        public async Task<IActionResult> CriarNovoEvento([FromBody] NovoEventoDto dto)
        {
            try
            {
                var id = await _eventoService.CriarNovoEvento(dto);
                return CreatedAtAction(nameof(GetTodosEventosDisponiveis), new { id }, new { id });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro interno no servidor.", detalhe = ex.Message });
            }
        }

        [HttpPut("put/update/evento")]
        public async Task<IActionResult> AtualizaEvento([FromBody] NovoEventoDto dto)
        {
            try
            {
                await _eventoService.UpdateEvento(dto);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro interno no servidor.", detalhe = ex.Message });
            }
        }
    }
}