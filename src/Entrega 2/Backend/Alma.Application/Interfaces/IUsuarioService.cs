using Alma.Application.DTOs.Usuario;
using Alma.Domain.Entities;

namespace Alma.Application.Interfaces.Repositorios
{
    public interface IUsuarioService
    {
        Task<Guid> CriarUsuario(NovoUsuarioDto dto);
        Task<Usuario> LoginUsuario(string email, string senha);
        Task UpdateUsuario(NovoUsuarioDto dto);
        Task DeleteUsuario(Guid id);
        Task<List<Usuario>> GetUsuarios();

        Task<Usuario> GetUsuarioById(Guid id);
    }
}
