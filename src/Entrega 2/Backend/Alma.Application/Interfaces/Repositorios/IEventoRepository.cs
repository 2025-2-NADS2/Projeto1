using Alma.Application.DTOs.Evento;
using Alma.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alma.Application.Interfaces.Repositorios
{
    public interface IEventoRepository
    {
        Task<List<Evento>> GetAllEventosDisponiveis();
        Task<List<Evento>> GetEventos();
        Task<Evento?> GetEventoById(int id);
        Task PostEvento(Evento evento);
        Task UpdateEvento(Evento evento);
    }
}
