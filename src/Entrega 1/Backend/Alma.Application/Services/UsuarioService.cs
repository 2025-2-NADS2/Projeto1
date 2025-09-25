using Alma.Application.DTOs.Usuario;
using Alma.Application.Interfaces.Repositorios;
using Alma.Domain.Entities;
using System.Text.RegularExpressions;

namespace Alma.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUnitOfWork _unitOfWork;
        private const int BcryptWorkFactor = 12; // custo recomendado

        public UsuarioService(IUsuarioRepository usuarioRepository, IUnitOfWork unitOfWork)
        {
            _usuarioRepository = usuarioRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> CriarUsuario(NovoUsuarioDto dto)
        {
            ValidateNovoUsuario(dto, isUpdate: false);

            var usuarios = await _usuarioRepository.GetUsuarios();

            if (usuarios.Any(x => x.Email.Equals(dto.Email, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException("Já existe um usuário com esse e-mail.");

            var usuario = new Usuario
            {
                Id = Guid.NewGuid(),
                Nome = dto.Nome?.Trim(),
                Email = dto.Email?.Trim(),
                Telefone = dto.Telefone,
                Permissoes = dto.Permissoes,
                Status = dto.Status,
                SenhaHash = BCrypt.Net.BCrypt.HashPassword(dto.Senha, BcryptWorkFactor),
                CriadoEm = DateTime.UtcNow,
                AtualizadoEm = DateTime.UtcNow
            };

            await _usuarioRepository.PostUsuario(usuario);
            await _unitOfWork.CommitAsync();

            return usuario.Id;
        }

        public async Task<Usuario> LoginUsuario(string email, string senha)
        {
            var usuario = await _usuarioRepository.GetUsuarioByEmail(email);
            if (usuario == null || !BCrypt.Net.BCrypt.Verify(senha, usuario.SenhaHash))
                throw new Exception("Email ou senha inválidos");

            // Rehash automático se custo antigo
            if (BCrypt.Net.BCrypt.PasswordNeedsRehash(usuario.SenhaHash, BcryptWorkFactor))
            {
                usuario.SenhaHash = BCrypt.Net.BCrypt.HashPassword(senha, BcryptWorkFactor);
            }
            usuario.AtualizadoEm = DateTime.UtcNow;
            await _usuarioRepository.UpdateUsuario(usuario);
            await _unitOfWork.CommitAsync();

            return usuario;
        }

        public async Task UpdateUsuario(NovoUsuarioDto dto)
        {
            if (dto.Id == Guid.Empty && string.IsNullOrWhiteSpace(dto.Email))
                throw new ArgumentException("Id ou Email deve ser informado para atualização.");

            ValidateNovoUsuario(dto, isUpdate: true);

            var usuarioExistente = dto.Id != Guid.Empty
                ? await _usuarioRepository.GetUsuarioById(dto.Id)
                : await _usuarioRepository.GetUsuarioByEmail(dto.Email);

            if (usuarioExistente == null) //atualizado para saber que existe
                throw new InvalidOperationException("Usuário não encontrado.");

            usuarioExistente.Nome = dto.Nome?.Trim();
            usuarioExistente.Telefone = dto.Telefone;
            usuarioExistente.Permissoes = dto.Permissoes;
            usuarioExistente.Status = dto.Status;
            usuarioExistente.AtualizadoEm = DateTime.UtcNow;
            if (!string.IsNullOrWhiteSpace(dto.Senha))
            {
                usuarioExistente.SenhaHash = BCrypt.Net.BCrypt.HashPassword(dto.Senha, BcryptWorkFactor);
            }

            await _usuarioRepository.UpdateUsuario(usuarioExistente);
            await _unitOfWork.CommitAsync();
        }
        public async Task DeleteUsuario(Guid id)
        {
            var existeUsuario = await _usuarioRepository.GetUsuarioById(id);
            if (existeUsuario == null)
                throw new InvalidOperationException("Usuário não encontrado.");

            await _usuarioRepository.DeleteUsuarioByUser(existeUsuario);
            await _unitOfWork.CommitAsync();
        }
        public async Task<List<Usuario>> GetUsuarios()
        {
            return await _usuarioRepository.GetUsuarios();
        }

        private static void ValidateNovoUsuario(NovoUsuarioDto dto, bool isUpdate)
        {
            if (!isUpdate)
            {
                if (string.IsNullOrWhiteSpace(dto.Nome) || string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Senha))
                    throw new ArgumentException("Nome, Email e Senha são obrigatórios.");
            }
            else
            {
                if (string.IsNullOrWhiteSpace(dto.Nome))
                    throw new ArgumentException("Nome é obrigatório.");
            }

            if (!string.IsNullOrWhiteSpace(dto.Email))
            {
                var isEmailValid = Regex.IsMatch(dto.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase);
                if (!isEmailValid)
                    throw new ArgumentException("Email inválido.");
            }

            if (!string.IsNullOrWhiteSpace(dto.Senha) && dto.Senha != dto.ConfirmarSenha)
                throw new ArgumentException("As senhas não conferem.");
        }
    }
}
