using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alma.Domain.Enum;

namespace Alma.Domain.Entities
{
    [Table("campanhas_doacoes")]
    public class Campanha
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // auto_increment
        public int Id { get; set; }

        [Column("titulo")]
        [MaxLength(150)]
        [Required]
        public string Titulo { get; set; } = string.Empty;

        [Column("descricao")]
        public string? Descricao { get; set; } // pode ser nulo no banco

        [Column("meta_valor", TypeName = "decimal(10,2)")]
        public decimal? MetaValor { get; set; } // pode ser nulo

        [Column("data_inicio")]
        [Required]
        public DateTime DataInicio { get; set; }

        [Column("data_fim")]
        public DateTime? DataFim { get; set; }

        // Persistido como string 'ativa' | 'finalizada'
        [Column("status")]
        [MaxLength(15)]
        private string StatusValor { get; set; } = "ativa";

        [NotMapped]
        public StatusCampanha Status
        {
            get => StatusValor == "finalizada" ? StatusCampanha.Finalizada : StatusCampanha.Ativa;
            set => StatusValor = value == StatusCampanha.Finalizada ? "finalizada" : "ativa";
        }

        [Column("criado_em")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // DEFAULT CURRENT_TIMESTAMP
        public DateTime CriadoEm { get; set; }

        [Column("atualizado_em")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)] // ON UPDATE CURRENT_TIMESTAMP
        public DateTime AtualizadoEm { get; set; }

        // Navegação (ajustar FK em Doacao para int)
        public List<Doacao>? Doacoes { get; set; }
    }
}
