using Alma.Application.Interfaces.Repositorios;
using Alma.Domain.DTOs.Usuario;
using Microsoft.AspNetCore.Mvc;

namespace Alma.API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }
        [HttpPost("post/criarusuario")]
        public async Task<IActionResult> CriarUsuario([FromBody] NovoUsuarioDto dto)
        {
            try
            {
                var usuarioId = await _usuarioService.CriarUsuario(dto);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro interno no servidor.", detalhe = ex.Message });
            }
        }
    }
}
