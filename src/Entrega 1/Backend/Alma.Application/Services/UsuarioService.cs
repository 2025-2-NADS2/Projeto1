using Alma.Application.Interfaces.Repositorios;
using Alma.Domain.DTOs.Usuario;
using Alma.Domain.Entities;

namespace Alma.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Guid> CriarUsuario(NovoUsuarioDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nome) || string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Senha))
                throw new ArgumentException("Nome, Email e Senha são obrigatórios.");

            // Verifica duplicidade
            var existeUsuario = await _usuarioRepository.GetUsuarios();

            if (existeUsuario.Any(x => x.Email == dto.Email))
                throw new InvalidOperationException("Já existe um usuário com esse e-mail.");

            var usuario = new Usuario
            {
                Id = Guid.NewGuid(),
                Name = dto.Nome,
                Email = dto.Email,
                Senha = dto.Senha,
                DateCreted = DateTime.Now,
            };

            _usuarioRepository.PostUsuario(usuario);
            await _unitOfWork.CommitAsync();

            return usuario.Id;
        }
    }
}
