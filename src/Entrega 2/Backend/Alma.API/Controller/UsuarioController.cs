using Alma.API.Auth;
using Alma.Application.DTOs.Usuario;
using Alma.Application.Interfaces.Repositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Alma.API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly JwtTokenGenerator _jwt;
        public UsuarioController(IUsuarioService usuarioService, JwtTokenGenerator jwt)
        {
            _usuarioService = usuarioService;
            _jwt = jwt;
        }

        [HttpPost("post/cadastro/usuario")]
        public async Task<IActionResult> CriarUsuario([FromBody] NovoUsuarioDto dto)
        {
            try
            {
                var id = await _usuarioService.CriarUsuario(dto);
                return CreatedAtAction(nameof(GetUsuarios), new { id }, new { id });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro interno no servidor.", detalhe = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            try
            {
                var usuario = await _usuarioService.LoginUsuario(dto.Email, dto.Senha);
                var token = _jwt.GenerateToken(usuario.Id, usuario.Email, usuario.Nome);

                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { mensagem = ex.Message });
            }
        }

        [HttpPut("put/update/usuario")]
        public async Task<IActionResult> AtualizaUsuario([FromBody] NovoUsuarioDto dto)
        {
            try
            {
                await _usuarioService.UpdateUsuario(dto);
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

        [HttpDelete("delete/usuario/{id:guid}")]
        public async Task<IActionResult> DeleteUsuario(Guid id)
        {
            try
            {
                await _usuarioService.DeleteUsuario(id);
                return NoContent();
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

        [HttpGet("get/usuarios")]
        public async Task<IActionResult> GetUsuarios()
        {
            try
            {
                var usuarios = await _usuarioService.GetUsuarios();
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro interno ao buscar usuários.", detalhe = ex.Message });
            }
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetUsuarioLogado()
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                    return Unauthorized(new { mensagem = "Token inválido" });

                var userId = Guid.Parse(userIdClaim.Value);
                var usuario = await _usuarioService.GetUsuarioById(userId);
                if (usuario == null)
                    return NotFound(new { mensagem = "Usuário não encontrado" });

                return Ok(new
                {
                    usuario.Id,
                    usuario.Nome,
                    usuario.Email,
                    usuario.Permissoes,
                    usuario.Status
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro interno no servidor.", detalhe = ex.Message });
            }
        }
    }
}
