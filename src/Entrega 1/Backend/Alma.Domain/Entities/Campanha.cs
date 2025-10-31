using Alma.Domain.Enum;
using System.Net.NetworkInformation;

namespace Alma.Domain.Entities
{
    public class Campanha
    {
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


    }
}
