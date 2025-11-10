using Alma.Application.Interfaces;
using Alma.Application.Interfaces.Repositorios;
using Alma.Domain.Entities;
using Alma.Infra.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
public class OuvidoriaController : ControllerBase
{
    private readonly AlmaDbContext _context;
    private readonly IOuvidoriaService _ouvidoriaService;
    public OuvidoriaController(AlmaDbContext context, IOuvidoriaService ouvidoriaService)
    {
        _context = context;
        _ouvidoriaService = ouvidoriaService;
    }

    [Authorize]
    [HttpPost("enviar")]
    public async Task<IActionResult> Enviar([FromBody] Ouvidoria mensagem)
    {
        var emailUsuario = User.FindFirst("email")?.Value;
        var nomeUsuario = User.FindFirst("Nome")?.Value;

        if (string.IsNullOrEmpty(emailUsuario))
            return Unauthorized("E-mail não encontrado no token.");

        mensagem.Email = emailUsuario;
        mensagem.Nome = nomeUsuario;

        await _ouvidoriaService.EnviarMensagemAsync(mensagem);
        return Ok("Mensagem enviada com sucesso!");
    }
}

