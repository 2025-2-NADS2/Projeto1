using Alma.Application.Interfaces.Repositorios;
using Alma.Domain.Entities;
using Alma.Infra.Data;
using Microsoft.EntityFrameworkCore;


namespace Alma.Infra.Repositories
{
    public class EventoRepository : BaseRepository<Evento>, IEventoRepository
    {
        private readonly AlmaDbContext _context;
        public EventoRepository(AlmaDbContext context) : base(context) { }

        public async Task<List<Evento>> GetAllEventosDisponiveis()
        {
            return await _context.Eventos
            .Where(e => e.Date > DateTime.Now)
            .ToListAsync();
        }
        public async Task<List<Evento>> GetEventos()
        {
            return await _context.Eventos
            .ToListAsync();
        }

        public async Task PostEvento(Evento evento)
        {
            _context.Eventos.Add(evento);
        }

        public async Task UpdateEvento(Evento evento)
        {
            _context.Eventos.Update(evento);
        }
    }
}
