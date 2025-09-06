using Alma.Domain.DTOs.Usuario;

namespace Alma.Application.Interfaces.Repositorios
{
    public interface IUsuarioService
    {
        Task<Guid> CriarUsuario(NovoUsuarioDto dto);
    }
}
