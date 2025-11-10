using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alma.Domain.Entities
{
    [Table("inscricoes_eventos")]
    public class Inscricoes
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // auto_increment
        public int Id { get; set; }

        [Required]
        [MaxLength(36)]
        [Column("usuario_id")]
        public string UsuarioId { get; set; } = string.Empty; // FK usuarios.Id (char/varchar 36)

        [Required]
        [ForeignKey(nameof(Evento))]
        [Column("evento_id")]
        public int EventoId { get; set; } // FK eventos.id

        public Evento? Evento { get; set; }

        [Column("data_inscricao")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // DEFAULT CURRENT_TIMESTAMP
        public DateTime DataInscricao { get; set; }
    }
}