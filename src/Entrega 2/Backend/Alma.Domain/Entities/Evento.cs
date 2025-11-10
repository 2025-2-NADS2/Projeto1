using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alma.Domain.Enum;

namespace Alma.Domain.Entities
{
    [Table("eventos")]
    public class Evento
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // auto_increment
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        [Column("titulo")]
        public string Titulo { get; set; } = string.Empty;

        [Column("descricao")]
        public string? Descricao { get; set; } // text, pode ser nulo

        // Banco: DATE. Mantido como DateTime para compatibilidade do projeto.
        [Required]
        [Column("data_evento")]
        public DateTime DataEvento { get; set; }

        // Banco: TIME, pode ser nulo
        [Column("horario")]
        public TimeSpan? Horario { get; set; }

        [MaxLength(200)]
        [Column("local")]
        public string? Local { get; set; }

        // Persistido como string exata 'planejado' | 'finalizado' | 'cancelado'
        [MaxLength(20)]
        [Column("status")]
        private string StatusValor { get; set; } = "planejado";

        [NotMapped]
        public StatusEvento Status
        {
            get => StatusValor switch
            {
                "finalizado" => StatusEvento.Finalizado,
                "cancelado"  => StatusEvento.Cancelado,
                _            => StatusEvento.Planejado
            };
            set => StatusValor = value switch
            {
                StatusEvento.Finalizado => "finalizado",
                StatusEvento.Cancelado  => "cancelado",
                _                       => "planejado"
            };
        }

        [Column("criado_em")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // DEFAULT CURRENT_TIMESTAMP
        public DateTime CriadoEm { get; set; }

        [Column("atualizado_em")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)] // ON UPDATE CURRENT_TIMESTAMP
        public DateTime AtualizadoEm { get; set; }
    }
}
