using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alma.Domain.Enum;

namespace Alma.Domain.Entities
{
    [Table("ouvidoria_mensagens")]
    public class Ouvidoria
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // auto_increment
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("nome")]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [MaxLength(150)]
        [Column("email")]
        public string Email { get; set; } = string.Empty;

        [MaxLength(20)]
        [Column("telefone")]
        public string? Telefone { get; set; }

        [Required]
        [Column("mensagem")]
        public string Mensagem { get; set; } = string.Empty;

        // Persistido como string exata 'novo' | 'lido' | 'respondido'
        [MaxLength(15)]
        [Column("status")]
        private string StatusValor { get; set; } = "novo";

        [NotMapped]
        public StatusOuvidoria Status
        {
            get => StatusValor switch
            {
                "lido"        => StatusOuvidoria.Lido,
                "respondido"  => StatusOuvidoria.Respondido,
                _             => StatusOuvidoria.Novo
            };
            set => StatusValor = value switch
            {
                StatusOuvidoria.Lido        => "lido",
                StatusOuvidoria.Respondido  => "respondido",
                _                           => "novo"
            };
        }

        [Column("data_envio")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // DEFAULT CURRENT_TIMESTAMP
        public DateTime DataEnvio { get; set; }
    }
}
