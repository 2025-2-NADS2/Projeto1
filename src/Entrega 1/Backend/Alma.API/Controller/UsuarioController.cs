using Alma.API.Auth;
using Alma.Application.DTOs.Usuario;
using Alma.Application.Interfaces.Repositorios;
using Alma.Application.Services;
using Alma.Domain.DTOs.Usuario;
using Microsoft.AspNetCore.Mvc;

namespace Alma.API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly JwtTokenGenerator _jwt;
        public UsuarioController(UsuarioService usuarioService, JwtTokenGenerator jwt)
        {
            _usuarioService = usuarioService;
            _jwt = jwt;
        }

        [HttpPost("post/cadastro/usuario")]
        public async Task<IActionResult> CriarUsuario([FromBody] NovoUsuarioDto dto)
        {
            try
            {
                await _usuarioService.CriarUsuario(dto);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro interno no servidor.", detalhe = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            try
            {
                var usuario = await _usuarioService.LoginUsuario (dto.Email, dto.Senha);
                var token = _jwt.GenerateToken(usuario.Id, usuario.Email);

                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}

