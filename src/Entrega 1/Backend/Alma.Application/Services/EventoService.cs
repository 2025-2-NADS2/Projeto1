using Alma.Application.DTOs.Evento;
using Alma.Application.Interfaces.Repositorios;
using Alma.Domain.DTOs.Usuario;
using Alma.Domain.Entities;
using Alma.Domain.Enum;

namespace Alma.Application.Services
{
    public class EventoService : IEventoService
    {
        private readonly IEventoRepository _eventoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EventoService(IEventoRepository eventoRepository, IUnitOfWork unitOfWork)
        {
            _eventoRepository = eventoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Evento>> GetTodosEventosDisponiveis()
        {
            return await _eventoRepository.GetAllEventosDisponiveis();
        }

        public async Task<Guid> CriarNovoEvento(NovoEventoDto dto)
        {

            isValid(dto);

            var evento = new Evento
            {
                Id = Guid.NewGuid(),
                Titulo = dto.Titulo,
                Descricao = dto.Descricao,
                Date = dto.Date,
                HorarioEvento = dto.HorarioEvento,
                LocalEvento = dto.LocalEvento,
                TipoEvento = dto.TipoEvento,
                DateCreated = DateTime.Now,
            };

            _eventoRepository.PostEvento(evento);
            await _unitOfWork.CommitAsync();

            return evento.Id;
        }

        public async Task UpdateEvento(NovoEventoDto dto)
        {

            isValid(dto);

            var evento = new Evento
            {
                Titulo = dto.Titulo,
                Descricao = dto.Descricao,
                Date = dto.Date,
                HorarioEvento = dto.HorarioEvento,
                LocalEvento = dto.LocalEvento,
                TipoEvento = dto.TipoEvento,
                DateCreated = DateTime.Now,
            };

            _eventoRepository.UpdateEvento(evento);
            await _unitOfWork.CommitAsync();

        }


        private void isValid(NovoEventoDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Titulo))
                throw new ArgumentException("Título é obrigatório.");

            if (string.IsNullOrWhiteSpace(dto.Descricao))
                throw new ArgumentException("Descrição é obrigatória.");

            if (!dto.Date.HasValue || dto.Date.Value == default)
                throw new ArgumentException("Data do evento é obrigatória.");

            if (!dto.HorarioEvento.HasValue || dto.HorarioEvento <= 0 || dto.HorarioEvento > 23.59)
                throw new ArgumentException("Horário do evento é inválido.");

            if (string.IsNullOrWhiteSpace(dto.LocalEvento))
                throw new ArgumentException("Local do evento é obrigatório.");

            if (dto.TipoEvento == null || dto.TipoEvento == 0) // se 0 for "NaoDefinido"
                throw new ArgumentException("Tipo de evento é obrigatório.");

            if (dto.DateCreated == default)
                throw new ArgumentException("Data de criação do evento é obrigatória.");
        }
    }
}
