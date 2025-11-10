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

// =========================
// 🌐 Configuração de CORS
// =========================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins(
            "http://localhost:3000",
            "https://localhost:3000"
        )
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

// =========================
// 💾 Configuração do banco MySQL
// =========================
builder.Services.AddDbContext<AlmaDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    )
);

// =========================
// 🧩 Injeção de dependências
// =========================
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IEventoRepository, EventoRepository>();
builder.Services.AddScoped<ICampanhaRepository, CampanhaRepository>();
builder.Services.AddScoped<IHistoriasRepository, HistoriasRepository>();
builder.Services.AddScoped<IInscricoesRepository, InscricoesRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IDoacaoRepository, DoacaoRepository>();
builder.Services.AddScoped<IRelatorioTransparenciaRepository, TransparenciaRepository>();
builder.Services.AddScoped<IRelatorioTransparenciaService, TransparenciaService>();

builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IEventoService, EventoService>();
builder.Services.AddScoped<IInscricoesService, InscricoesService>();
builder.Services.AddScoped<ICampanhaService, CampanhaService>();
builder.Services.AddScoped<IDoacaoService, DoacaoService>();

builder.Services.AddScoped<JwtTokenGenerator>();

builder.Services.AddControllers();
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/");
    options.Conventions.AllowAnonymousToPage("/Index");
});

// =========================
// 🔐 Autenticação JWT + Cookie
// =========================
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
            context.HandleResponse();
            context.Response.StatusCode = 401;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync("Token não fornecido ou inválido");
        }
    };
});

builder.Services.AddAuthorization();

// =========================
// 🚀 Construção do App
// =========================
var app = builder.Build();

app.UseRouting();
app.UseCors("AllowReactApp"); // ✅ Agora está na posição correta
app.UseAuthentication();
app.UseAuthorization();

// =========================
// 🔑 Endpoint de Login (teste rápido)
// =========================
app.MapPost("/login", async (HttpContext context, IUsuarioRepository usuarioRepo, JwtTokenGenerator jwtGen) =>
{
    var body = await context.Request.ReadFromJsonAsync<LoginDto>();
    var usuario = await usuarioRepo.AutenticarAsync(body.Email, body.Senha);

    if (usuario == null)
        return Results.Unauthorized();

    var token = jwtGen.GenerateToken(usuario.Id, usuario.Email, usuario.Nome);
    return Results.Ok(new { token });
});

app.MapControllers();
app.MapRazorPages();

app.Run();

// DTO temporário de login
public record LoginDto(string Email, string Senha);
