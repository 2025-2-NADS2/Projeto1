using Alma.Application.DTOs.Evento;
using Alma.Application.Interfaces.Repositorios;
using Alma.Domain.Entities;

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
            Validate(dto);

            var evento = new Evento
            {
                Id = Guid.NewGuid(),
                Titulo = dto.Titulo,
                Descricao = dto.Descricao,
                Date = dto.Date,
                HorarioEvento = dto.HorarioEvento,
                LocalEvento = dto.LocalEvento,
                TipoEvento = dto.TipoEvento,
                DateCreated = DateTime.UtcNow,
            };

            await _eventoRepository.PostEvento(evento);
            await _unitOfWork.CommitAsync();

            return evento.Id;
        }

        public async Task UpdateEvento(NovoEventoDto dto)
        {
            if (dto.Id == Guid.Empty) throw new ArgumentException("Id do evento é obrigatório.");

            Validate(dto);

            var existente = await _eventoRepository.GetEventoById(dto.Id);
            if (existente == null) throw new InvalidOperationException("Evento não encontrado.");

            existente.Titulo = dto.Titulo;
            existente.Descricao = dto.Descricao;
            existente.Date = dto.Date;
            existente.HorarioEvento = dto.HorarioEvento;
            existente.LocalEvento = dto.LocalEvento;
            existente.TipoEvento = dto.TipoEvento;
            // não altere DateCreated aqui

            await _eventoRepository.UpdateEvento(existente);
            await _unitOfWork.CommitAsync();
        }

        private static void Validate(NovoEventoDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Titulo))
                throw new ArgumentException("Título é obrigatório.");

            if (string.IsNullOrWhiteSpace(dto.Descricao))
                throw new ArgumentException("Descrição é obrigatória.");

            if (!dto.Date.HasValue || dto.Date.Value == default)
                throw new ArgumentException("Data do evento é obrigatória.");

            if (!dto.HorarioEvento.HasValue || dto.HorarioEvento <= 0 || dto.HorarioEvento > 23.99)
                throw new ArgumentException("Horário do evento é inválido.");

            if (string.IsNullOrWhiteSpace(dto.LocalEvento))
                throw new ArgumentException("Local do evento é obrigatório.");
        }
    }
}
