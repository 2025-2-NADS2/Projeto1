using Alma.Domain.Enum;

namespace Alma.Domain.Entities
{
    public class Evento
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime? Date { get; set; }
        public float? HorarioEvento { get; set; }
        public string LocalEvento { get; set; }
        public TipoEvento TipoEvento { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }

    }
}
