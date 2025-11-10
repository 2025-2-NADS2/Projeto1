using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alma.Domain.Enum;

namespace Alma.Domain.Entities
{
    [Table("doacoes")]
    public class Doacao
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        [Column("doador_nome")]
        public string DoadorNome { get; set; } = string.Empty;

        [MaxLength(100)]
        [Column("doador_email")]
        public string? DoadorEmail { get; set; }

        [MaxLength(20)]
        [Column("doador_telefone")]
        public string? DoadorTelefone { get; set; }

        [MaxLength(14)]
        [Column("doador_cpf")]
        public string? DoadorCpf { get; set; }

        [Required]
        [Column("valor", TypeName = "decimal(10,2)")]
        public decimal Valor { get; set; }

        [Required, MaxLength(50)]
        [Column("metodo_pagamento")]
        public string MetodoPagamento { get; set; } = string.Empty;

        [MaxLength(15)]
        [Column("status_pagamento")]
        private string StatusPagamentoValor { get; set; } = "pendente";

        [NotMapped]
        public StatusPagamentoDoacao StatusPagamento
        {
            get => StatusPagamentoValor switch
            {
                "aprovado"   => StatusPagamentoDoacao.Aprovado,
                "recusado"   => StatusPagamentoDoacao.Recusado,
                "estornado"  => StatusPagamentoDoacao.Estornado,
                _            => StatusPagamentoDoacao.Pendente
            };
            set => StatusPagamentoValor = value switch
            {
                StatusPagamentoDoacao.Aprovado   => "aprovado",
                StatusPagamentoDoacao.Recusado   => "recusado",
                StatusPagamentoDoacao.Estornado  => "estornado",
                _                                => "pendente"
            };
        }

        [Column("campanha_id")]
        public int? CampanhaId { get; set; } // FK pode ser nula

        public Campanha? Campanha { get; set; }

        [MaxLength(100)]
        [Column("transacao_id")]
        public string? TransacaoId { get; set; }

        [Column("recorrente")]
        public bool Recorrente { get; set; } // tinyint(1)

        [Column("data_doacao")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime DataDoacao { get; set; }
    }
}
