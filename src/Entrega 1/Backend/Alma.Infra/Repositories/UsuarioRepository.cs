using Alma.Application.Interfaces.Repositorios;
using Alma.Domain.Entities;
using Alma.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Alma.Infra.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        private readonly AlmaDbContext _context;
        public UsuarioRepository(AlmaDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task PostUsuario(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
        }
        public async Task<List<Usuario>> GetUsuarios()
        {
            return await _context.Usuarios
                .AsNoTracking()
                .OrderBy(u => u.Nome)
                .ToListAsync();
        }

        public async Task<Usuario?> GetUsuarioByEmail(string email)
        {
            return await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task UpdateUsuario(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            await Task.CompletedTask;
        }
        public async Task DeleteUsuarioByUser(Usuario usuario)
        {
            _context.Usuarios.Remove(usuario);
            await Task.CompletedTask;
        }
        
        public async Task<Usuario?> GetUsuarioById(Guid id)
        {
            return await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
