using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alma.Domain.Entities
{
    [Table("documentos_transparencia")]
    public class RelatorioTransparencia
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
        public string? Descricao { get; set; } // TEXT, pode ser nulo

        [Required]
        [Column("arquivo_url")]
        public string ArquivoUrl { get; set; } = string.Empty; // TEXT, NOT NULL

        [Required]
        [Column("data_publicacao", TypeName = "date")] // DATE no banco
        public DateTime DataPublicacao { get; set; }

        [Column("criado_em")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // DEFAULT CURRENT_TIMESTAMP
        public DateTime CriadoEm { get; set; }
    }
}
