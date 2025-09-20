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

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AlmaDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    )
);

// Repositórios
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IEventoRepository, EventoRepository>();
builder.Services.AddScoped<ICampanhaRepository, CampanhaRepository>();
builder.Services.AddScoped<IHistoriasRepository, HistoriasRepository>();
builder.Services.AddScoped<IInscricoesRepository, InscricoesRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Serviços
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IEventoService, EventoService>();
builder.Services.AddScoped<IInscricoesService, InscricoesService>();
builder.Services.AddScoped<JwtTokenGenerator>();

// Controllers e Razor Pages
builder.Services.AddControllers();
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/");
    options.Conventions.AllowAnonymousToPage("/Index");
});

// Chave JWT
var jwtKey = builder.Configuration["Jwt:Key"];
if (string.IsNullOrWhiteSpace(jwtKey))
    throw new InvalidOperationException("Configuração 'Jwt:Key' ausente.");
var secretKey = Encoding.UTF8.GetBytes(jwtKey);

// Autenticação inteligente: JWT para API, Cookie para páginas
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
        
        // Se não for API, usa Cookie
        return CookieAuthenticationDefaults.AuthenticationScheme;
    };
})
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
{
    options.LoginPath = "/Index"; // ou "/Login" se tiver página específica
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
            // Customizar mensagem de erro 401
            context.HandleResponse();
            context.Response.StatusCode = 401;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync("Token não fornecido ou inválido");
        }
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();

app.Run();