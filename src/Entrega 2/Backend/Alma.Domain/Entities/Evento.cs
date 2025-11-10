using System.ComponentModel.DataAnnotations.Schema;

namespace Alma.Domain.Entities
{
    [Table("eventos")]
    public class Evento
    {
        [Column("id")]
        public int Id { get; set; } // int, igual ao banco

        [Column("titulo")]
        public string Titulo { get; set; }

        [Column("descricao")]
        public string Descricao { get; set; }

        [Column("data_evento")]
        public DateTime DataEvento { get; set; }

        [Column("horario")]
        public TimeSpan? Horario { get; set; }

        [Column("local")]
        public string Local { get; set; }

        [Column("status")]
        public string Status { get; set; }

        [Column("criado_em")]
        public DateTime? CriadoEm { get; set; }

        [Column("atualizado_em")]
        public DateTime? AtualizadoEm { get; set; }
    }
}
