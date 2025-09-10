using Alma.Application.Interfaces.Repositorios;
using Alma.Application.Services;
using Alma.Domain.Entities;
using Alma.Infra.Data;
using Alma.Infra.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Alma.API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {
        private readonly AlmaDbContext _context;
        private readonly IEventoService _eventoService;

        public EventoController(AlmaDbContext context, IEventoService eventoService)
        {
            _context = context;
            _eventoService = eventoService;
        }

        [HttpGet("get/eventos/")]
        public async Task<ActionResult<List<Evento>>> GetTodosEventosDisponiveis()
        {
            try
            {
                var eventos = await _eventoService.GetTodosEventosDisponiveis();
                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao buscar eventos: {ex.Message}");
            }
        }
    }
}