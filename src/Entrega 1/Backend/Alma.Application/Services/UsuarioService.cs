using Alma.Application.DTOs.Usuario;
using Alma.Application.Interfaces.Repositorios;
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

            var usuarios = await _usuarioRepository.GetUsuarios();

            if (usuarios.Any(x => x.Email == dto.Email))
                throw new InvalidOperationException("Já existe um usuário com esse e-mail.");

            var usuario = new Usuario
            {
                Id = Guid.NewGuid(),
                Name = dto.Nome,
                Email = dto.Email,
                Senha = BCrypt.Net.BCrypt.HashPassword(dto.Senha),
                DateCreated = DateTime.UtcNow,
            };

            await _usuarioRepository.PostUsuario(usuario);
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

        public async Task UpdateUsuario(NovoUsuarioDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nome) || string.IsNullOrWhiteSpace(dto.Email))
                throw new ArgumentException("Nome e Email são obrigatórios.");

            var existente = await _usuarioRepository.GetUsuarioByEmail(dto.Email);
            if (existente == null)
                throw new InvalidOperationException("Usuário não encontrado.");

            existente.Name = dto.Nome;
            if (!string.IsNullOrWhiteSpace(dto.Senha))
            {
                existente.Senha = BCrypt.Net.BCrypt.HashPassword(dto.Senha);
            }

            await _usuarioRepository.UpdateUsuario(existente);
            await _unitOfWork.CommitAsync();
        }
        public async Task DeleteUsuario(Guid id)
        {
            var usuario = await _usuarioRepository.GetUsuarioById(id);
            if (usuario == null)
                throw new InvalidOperationException("Usuário não encontrado.");

            await _usuarioRepository.DeleteUsuarioByUser(usuario);
            await _unitOfWork.CommitAsync();

        }
        public async Task<List<Usuario>> GetUsuarios()
        {
            return await _usuarioRepository.GetUsuarios();
        }
    }
}
