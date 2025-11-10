using Alma.Application.DTOs.Campanha;
using Alma.Application.Interfaces.Repositorios;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Alma.API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class CampanhaController : ControllerBase
    {
        private readonly ICampanhaService _campanhaService;

        public CampanhaController(ICampanhaService campanhaService)
        {
            _campanhaService = campanhaService;
        }

        [HttpGet("listar")]
        public async Task<IActionResult> GetTodasCampanhasDisponiveis()
        {
            var campanhas = await _campanhaService.GetTodasCampanhasDisponiveis();
            return Ok(campanhas);
        }

        [HttpPost("criar")]
        public async Task<IActionResult> CriarNovaCampanha([FromBody] NovaCampanhaDto dto)
        {
            var id = await _campanhaService.CriarNovaCampanha(dto);
            return Ok(new { id });
        }

        [HttpPut("atualizar")]
        public async Task<IActionResult> AtualizarCampanha([FromBody] NovaCampanhaDto dto)
        {
            await _campanhaService.UpdateCampanha(dto);
            return Ok();
        }

        [HttpDelete("excluir/{id:int}")]
        public async Task<IActionResult> DeleteCampanha(int id)
        {
            await _campanhaService.DeleteCampanha(id);
            return Ok();
        }
    }
}
