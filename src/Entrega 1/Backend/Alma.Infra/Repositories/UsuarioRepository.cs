using Alma.Application.Interfaces.Repositorios;
using Alma.Domain.Entities;
using Alma.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Alma.Infra.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        private readonly AlmaDbContext _context;
        public UsuarioRepository(AlmaDbContext context) : base(context) { }

        public async Task PostUsuario(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
        }
        public async Task<List<Usuario>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }
    }
}
