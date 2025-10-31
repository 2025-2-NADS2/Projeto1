using Alma.Domain.Entities;

namespace Alma.Application.Interfaces.Repositorios
{
    public interface IUsuarioRepository
    {
        Task PostUsuario(Usuario model);
        Task<List<Usuario>> GetUsuarios();
        Task<Usuario?> GetUsuarioByEmail(string email);
        Task UpdateUsuario(Usuario usuario);
        Task<Usuario?> GetUsuarioById(Guid id);
        Task DeleteUsuarioByUser(Usuario usuario);
        Task<Usuario?> AutenticarAsync(string email, string senha);
    }
}
