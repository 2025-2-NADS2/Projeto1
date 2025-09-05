namespace Alma.Domain.Entities
{
    public class Inscricoes
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid EventoId { get; set; }
        public DateTime DateCreated { get; set; }

    }
}
