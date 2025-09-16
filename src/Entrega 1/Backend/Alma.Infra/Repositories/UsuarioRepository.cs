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
            _context.Usuarios.Add(usuario);
        }
        public async Task<List<Usuario>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<Usuario> GetUsuarioByEmail(string email)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(x => x.Email.Equals(email));
        }

        public async Task UpdateUsuario(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
        }
        public async Task DeleteUsuarioByUser(Usuario usuario)
        {
            _context.Usuarios.Remove(usuario);
        }
        
        public async Task<Usuario> GetUsuarioById(Guid id)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

    }
}
