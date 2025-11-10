using Alma.Domain.Enum;
using System.Net.NetworkInformation;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alma.Domain.Entities
{
    [Table("campanhas_doacoes")]
    public class Campanha
    {
        public Guid Id { get; set; }
        public decimal? ValorArrecadado { get; set; }
        public StatusCampanha StatusCampanha { get; set; }
        public Guid? CreatedBy { get; set; }
        public List<Doacao>? Doacoes { get; set; }
        public string ImagemUrl { get; set; }


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

        [Column("criado_em")]
        public DateTime? DateCreated { get; set; }

        [Column("atualizado_em")]
        public DateTime? AtualizadoEm { get; set; }
    }
}
