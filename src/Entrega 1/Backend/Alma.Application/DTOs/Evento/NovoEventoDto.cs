using Alma.Domain.Enum;
namespace Alma.Application.DTOs.Evento
{
    public class NovoEventoDto
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime? Date { get; set; }
        public float? HorarioEvento { get; set; }
        public string LocalEvento { get; set; }
        public TipoEvento TipoEvento { get; set; }
        public DateTime DateCreated { get; set; }

    }
}
