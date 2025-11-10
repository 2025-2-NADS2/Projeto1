using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alma.Domain.Entities
{
    [Table("historias_destaque")]
    public class Historias
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // auto_increment
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        [Column("titulo")]
        public string Titulo { get; set; } = string.Empty; // NOT NULL

        [Required]
        [Column("conteudo")]
        public string Conteudo { get; set; } = string.Empty; // NOT NULL (TEXT)

        [Column("criado_em")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // DEFAULT CURRENT_TIMESTAMP
        public DateTime CriadoEm { get; set; }
    }
}
