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
            return await _context.Eventos
                .AsNoTracking()
                .Where(e => e.Date > DateTime.Now)
                .ToListAsync();
        }
        public async Task<List<Evento>> GetEventos()
        {
            return await _context.Eventos
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Evento?> GetEventoById(Guid id)
        {
            return await _context.Eventos.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task PostEvento(Evento evento)
        {
            await _context.Eventos.AddAsync(evento);
        }

        public async Task UpdateEvento(Evento evento)
        {
            _context.Eventos.Update(evento);
            await Task.CompletedTask;
        }
    }
}
