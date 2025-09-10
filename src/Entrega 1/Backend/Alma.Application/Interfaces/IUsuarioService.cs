using Alma.Domain.DTOs.Usuario;
using Alma.Domain.Entities;

namespace Alma.Application.Interfaces.Repositorios
{
    public interface IUsuarioService
    {
        Task<Guid> CriarUsuario(NovoUsuarioDto dto);
        Task<Usuario> LoginUsuario(string email, string senha);
    }
}
