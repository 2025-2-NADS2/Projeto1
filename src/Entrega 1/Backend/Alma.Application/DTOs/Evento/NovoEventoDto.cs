using System;

namespace Alma.Application.DTOs.Evento
{
    public class NovoEventoDto
    {
        public int Id { get; set; } // usado para updates
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataEvento { get; set; }
        public TimeSpan? Horario { get; set; }
        public string Local { get; set; }
        // Status pode ser derivado no domínio; manter opcional no DTO
        public string? Status { get; set; }
        public DateTime? CriadoEm { get; set; }
    }
}
