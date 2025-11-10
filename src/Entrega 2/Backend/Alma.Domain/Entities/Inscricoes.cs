using System.ComponentModel.DataAnnotations.Schema;

namespace Alma.Domain.Entities
{
    [Table("inscricoes_eventos")]
    public class Inscricoes
    {
        [Column("id")]
        public int Id { get; set; } // int, igual ao banco

        [Column("usuario_id")]
        public string UsuarioId { get; set; } // varchar(36), pode ser Guid.ToString()

        [Column("evento_id")]
        public int EventoId { get; set; } // int, igual ao banco

        [Column("data_inscricao")]
        public DateTime DataInscricao { get; set; }
    }
}
