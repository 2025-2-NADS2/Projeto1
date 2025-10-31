using Alma.Domain.Enum;
using System.Net.NetworkInformation;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alma.Domain.Entities
{
    [Table("campanhas_doacoes")]
    public class Campanha
    {
<<<<<<< HEAD
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public decimal MetaValor { get; set; }
        public decimal? ValorArrecadado { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set;}
        public StatusCampanha StatusCampanha { get; set; }
        public Guid? CreatedBy { get; set; }
        public List<Doacao>? Doacoes { get; set; }
        public string ImagemUrl { get; set; }

=======
        [Column("id")]
        public int Id { get; set; }
>>>>>>> c5311ff5b00b4eb6d1ee3e753ac2a05e5328884f

        [Column("titulo")]
        public string Titulo { get; set; }

        [Column("descricao")]
        public string Descricao { get; set; }

        [Column("meta_valor")]
        public decimal? MetaValor { get; set; }

        [Column("data_inicio")]
        public DateTime DataInicio { get; set; }

        [Column("data_fim")]
        public DateTime? DataFim { get; set; }

        [Column("status")]
        public string Status { get; set; }

        [Column("criado_em")]
        public DateTime? CriadoEm { get; set; }

        [Column("atualizado_em")]
        public DateTime? AtualizadoEm { get; set; }
    }
}
