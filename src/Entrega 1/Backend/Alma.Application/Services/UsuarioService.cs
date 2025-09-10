using Alma.Application.DTOs.Usuario;
using Alma.Application.Interfaces.Repositorios;
using Alma.Domain.DTOs.Usuario;
using Alma.Domain.Entities;

namespace Alma.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UsuarioService(IUsuarioRepository usuarioRepository, IUnitOfWork unitOfWork)
        {
            _usuarioRepository = usuarioRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> CriarUsuario(NovoUsuarioDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nome) || string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Senha))
                throw new ArgumentException("Nome, Email e Senha são obrigatórios.");

            var existeUsuario = await _usuarioRepository.GetUsuarios();

            if (existeUsuario.Any(x => x.Email == dto.Email))
                throw new InvalidOperationException("Já existe um usuário com esse e-mail.");

            var usuario = new Usuario
            {
                Id = Guid.NewGuid(),
                Name = dto.Nome,
                Email = dto.Email,
                Senha = BCrypt.Net.BCrypt.HashPassword(dto.Senha),
                DateCreted = DateTime.Now,
            };

            _usuarioRepository.PostUsuario(usuario);
            await _unitOfWork.CommitAsync();

            return usuario.Id;
        }

        public async Task<Usuario> LoginUsuario(string email, string senha)
        {
            var usuario = await _usuarioRepository.GetUsuarioByEmail(email);
            if (usuario == null || !BCrypt.Net.BCrypt.Verify(senha, usuario.Senha))
                throw new Exception("Email ou senha inválidos");

            return usuario; // deixa a API decidir como gerar o token
        }
    }
}
