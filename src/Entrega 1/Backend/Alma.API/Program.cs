using Alma.Infra.Data;
using Alma.Infra.Repositories;
using Alma.Application.Interfaces.Repositorios;
using Alma.API.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configurar DbContext com MySQL
builder.Services.AddDbContext<AlmaDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    )
);

// Injetar repositórios
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IEventoRepository, EventoRepository>();
builder.Services.AddScoped<ICampanhaRepository, CampanhaRepository>();
builder.Services.AddScoped<IHistoriasRepository, HistoriasRepository>();
builder.Services.AddScoped<IInscricoesRepository, InscricoesRepository>();
builder.Services.AddScoped<JwtTokenGenerator>();

builder.Services.AddControllers();

var secretKey = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false; // pode deixar true em produção
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(secretKey),
            ClockSkew = TimeSpan.Zero
        };
    });

// Habilitar autorização
builder.Services.AddAuthorization();

var app = builder.Build();

// ----------------------------
// 🚀 Middleware padrão do ASP.NET
// ----------------------------
app.UseRouting();
app.UseAuthentication(); // <--- autenticação JWT
app.UseAuthorization();  // <--- autorização baseada nas claims

// Rotas públicas (ex: login)
app.MapPost("/login", async (HttpContext context, IUsuarioRepository usuarioRepo, JwtTokenGenerator jwtGen) =>
{
    // Exemplo simples de login manual (ajuste conforme seu código atual)
    var body = await context.Request.ReadFromJsonAsync<LoginDto>();
    var usuario = await usuarioRepo.AutenticarAsync(body.Email, body.Senha);

    if (usuario == null)
        return Results.Unauthorized();

    // Correção: Passar usuario.Id (Guid) e usuario.Email (string) para GenerateToken
    var token = jwtGen.GenerateToken(usuario.Id, usuario.Email, usuario.Name); 
    
    return Results.Ok(new { token });
});

// Controllers (demais rotas)
app.MapControllers();

app.Run();

// DTO temporário de login
public record LoginDto(string Email, string Senha);