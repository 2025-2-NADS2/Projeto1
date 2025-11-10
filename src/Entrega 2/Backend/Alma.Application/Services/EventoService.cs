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

        public async Task<int> CriarNovoEvento(NovoEventoDto dto)
        {
            Validate(dto);

            var evento = new Evento
            {
                // Id será gerado pelo banco (IDENTITY) se configurado
                Titulo = dto.Titulo,
                Descricao = dto.Descricao,
                DataEvento = dto.DataEvento,
                Horario = dto.Horario,
                Local = dto.Local,
                Status = dto.Status ?? "ativo",
                CriadoEm = DateTime.UtcNow,
            };

            await _eventoRepository.PostEvento(evento);
            await _unitOfWork.CommitAsync();

            return evento.Id;
        }

        public async Task UpdateEvento(NovoEventoDto dto)
        {
            if (dto.Id <= 0) throw new ArgumentException("Id do evento é obrigatório.");

            Validate(dto);

            var existente = await _eventoRepository.GetEventoById(dto.Id);
            if (existente == null) throw new InvalidOperationException("Evento não encontrado.");

            existente.Titulo = dto.Titulo;
            existente.Descricao = dto.Descricao;
            existente.DataEvento = dto.DataEvento;
            existente.Horario = dto.Horario;
            existente.Local = dto.Local;
            existente.Status = dto.Status ?? existente.Status;
            existente.AtualizadoEm = DateTime.UtcNow; // atualiza timestamp de atualização
            // não altere CriadoEm aqui

            await _eventoRepository.UpdateEvento(existente);
            await _unitOfWork.CommitAsync();
        }

        private static void Validate(NovoEventoDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Titulo))
                throw new ArgumentException("Título é obrigatório.");

            if (string.IsNullOrWhiteSpace(dto.Descricao))
                throw new ArgumentException("Descrição é obrigatória.");

            if (dto.DataEvento == default)
                throw new ArgumentException("Data do evento é obrigatória.");

            if (dto.Horario.HasValue && (dto.Horario.Value < TimeSpan.Zero || dto.Horario.Value >= TimeSpan.FromDays(1)))
                throw new ArgumentException("Horário do evento é inválido.");

            if (string.IsNullOrWhiteSpace(dto.Local))
                throw new ArgumentException("Local do evento é obrigatório.");
        }
    }
}
