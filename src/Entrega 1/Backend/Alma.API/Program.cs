using Alma.Infra.Data;
using Alma.Infra.Repositories;
using Alma.Application.Interfaces.Repositorios;
using Alma.Application.Services;
using Alma.API.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Alma.Application.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// ----------------------------
// 💾 Configurar DbContext com MySQL
// ----------------------------
builder.Services.AddDbContext<AlmaDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    )
);

// ----------------------------
// 🧱 Injeção de dependências
// ----------------------------
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IEventoRepository, EventoRepository>();
builder.Services.AddScoped<ICampanhaRepository, CampanhaRepository>();
builder.Services.AddScoped<IHistoriasRepository, HistoriasRepository>();
builder.Services.AddScoped<IInscricoesRepository, InscricoesRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Adicionar injeções para Doacao (assumindo que você tem IDoacaoRepository e DoacaoRepository)
builder.Services.AddScoped<IDoacaoRepository, DoacaoRepository>();

// Adicionar injeções para o download de relatórios (Transparência)
builder.Services.AddScoped<IRelatorioTransparenciaRepository, TransparenciaRepository>();  // Assumindo que você tem essa interface e classe
builder.Services.AddScoped<IRelatorioTransparenciaService, TransparenciaService>();      // Assumindo que você tem essa interface e classe

builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IEventoService, EventoService>();
builder.Services.AddScoped<IInscricoesService, InscricoesService>();
// Adicionar serviço para Campanha (se faltava)
builder.Services.AddScoped<ICampanhaService, CampanhaService>();
// Adicionar serviço para Doacao (assumindo que você tem IDoacaoService e DoacaoService)
builder.Services.AddScoped<IDoacaoService, DoacaoService>();

builder.Services.AddScoped<JwtTokenGenerator>();

builder.Services.AddControllers();
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/");
    options.Conventions.AllowAnonymousToPage("/Index");
});

// ----------------------------
// 🔐 Configurar autenticação
// ----------------------------
var jwtKey = builder.Configuration["Jwt:Key"];
if (string.IsNullOrWhiteSpace(jwtKey))
    throw new InvalidOperationException("Configuração 'Jwt:Key' ausente.");

var secretKey = Encoding.UTF8.GetBytes(jwtKey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Smart";
    options.DefaultChallengeScheme = "Smart";
})
.AddPolicyScheme("Smart", "Smart Authentication", options =>
{
    options.ForwardDefaultSelector = context =>
    {
        // Se for API, usa JWT
        if (context.Request.Path.StartsWithSegments("/api"))
            return JwtBearerDefaults.AuthenticationScheme;

        // Caso contrário, usa Cookie
        return CookieAuthenticationDefaults.AuthenticationScheme;
    };
})
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
{
    options.LoginPath = "/Index";
    options.AccessDeniedPath = "/Index";
})
.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(secretKey),
        ClockSkew = TimeSpan.Zero
    };

    options.Events = new JwtBearerEvents
    {
        OnChallenge = context =>
        {
            // Customizar resposta 401
            context.HandleResponse();
            context.Response.StatusCode = 401;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync("Token não fornecido ou inválido");
        }
    };
});

builder.Services.AddAuthorization();

// ----------------------------
// 🚀 Construir o app
// ----------------------------
var app = builder.Build();

// Middleware padrão
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// ----------------------------
// 🔑 Endpoint de Login (exemplo)
// ----------------------------
app.MapPost("/login", async (HttpContext context, IUsuarioRepository usuarioRepo, JwtTokenGenerator jwtGen) =>
{
    var body = await context.Request.ReadFromJsonAsync<LoginDto>();
    var usuario = await usuarioRepo.AutenticarAsync(body.Email, body.Senha);

    if (usuario == null)
        return Results.Unauthorized();

    var token = jwtGen.GenerateToken(usuario.Id, usuario.Email, usuario.Nome);

    return Results.Ok(new { token });
});

// Controllers e páginas
app.MapControllers();
app.MapRazorPages();

app.Run();

// DTO temporário de login
public record LoginDto(string Email, string Senha);
