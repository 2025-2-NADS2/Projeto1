using Alma.Infra.Data;
using Alma.Infra.Repositories;
using Alma.Application.Interfaces.Repositorios;
using Alma.API.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configurar DbContext com SQLite
builder.Services.AddDbContext<AlmaDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Injetar repositórios
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IEventoRepository, EventoRepository>();
builder.Services.AddScoped<ICampanhaRepository, CampanhaRepository>();
builder.Services.AddScoped<IHistoriasRepository, HistoriasRepository>();
builder.Services.AddScoped<IInscricoesRepository, InscricoesRepository>();
builder.Services.AddScoped<JwtTokenGenerator>();

// Configurar controllers
builder.Services.AddControllers();

var app = builder.Build();

// Middleware de autenticação JWT manual
var secretKey = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);

app.Use(async (context, next) =>
{
    // Pular autenticação para endpoints públicos
    if (context.Request.Path.StartsWithSegments("/login") ||
        context.Request.Path.StartsWithSegments("/public"))
    {
        await next();
        return;
    }

    var authHeader = context.Request.Headers["Authorization"].ToString();
    var token = authHeader.Replace("Bearer ", "");

    if (string.IsNullOrEmpty(token))
    {
        context.Response.StatusCode = 401;
        await context.Response.WriteAsync("Token não fornecido");
        return;
    }

    try
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        tokenHandler.ValidateToken(token, new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(secretKey),
            ClockSkew = TimeSpan.Zero
        }, out var validatedToken);

        await next(); // Token válido, continua
    }
    catch
    {
        context.Response.StatusCode = 401;
        await context.Response.WriteAsync("Token inválido");
    }
});

// Configurar roteamento e controllers
app.MapControllers();

app.Run();