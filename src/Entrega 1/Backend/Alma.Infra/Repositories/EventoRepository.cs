using Alma.Application.Interfaces.Repositorios;
using Alma.Domain.Entities;
using Alma.Infra.Data;
using Microsoft.EntityFrameworkCore;


namespace Alma.Infra.Repositories
{
    public class EventoRepository : BaseRepository<Evento>, IEventoRepository
    {
        private readonly AlmaDbContext _context;
        public EventoRepository(AlmaDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Evento>> GetAllEventosDisponiveis()
        {
            var now = DateTime.UtcNow; // usa UTC para consistência
            return await _context.Eventos
                .AsNoTracking()
                .Where(e => e.DataEvento > now)
                .OrderBy(e => e.DataEvento)
                .ToListAsync();
        }
        public async Task<List<Evento>> GetEventos()
        {
            return await _context.Eventos
                .AsNoTracking()
                .OrderBy(e => e.DataEvento)
                .ToListAsync();
        }

        public async Task<Evento?> GetEventoById(int id)
        {
            return await _context.Eventos.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task PostEvento(Evento evento)
        {
            await _context.Eventos.AddAsync(evento);
        }

        public Task UpdateEvento(Evento evento)
        {
            _context.Eventos.Update(evento);
            return Task.CompletedTask;
        }
    }
}
