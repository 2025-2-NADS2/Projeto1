using Alma.Infra.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Alma.API.Controller
{
    [ApiController]
    [Route("api/health")]
    public class HealthController : ControllerBase
    {
        private readonly AlmaDbContext _dbContext;
        public HealthController(AlmaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("db")]
        public async Task<IActionResult> CheckDatabaseConnection()
        {
            try
            {
                // Tenta abrir uma conexão simples
                await _dbContext.Database.OpenConnectionAsync();
                await _dbContext.Database.CloseConnectionAsync();
                return Ok(new { status = "Conectado ao banco de dados" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = "Falha na conexão", detalhe = ex.Message });
            }
        }
    }
}
