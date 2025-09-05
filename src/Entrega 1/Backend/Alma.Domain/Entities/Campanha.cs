using Alma.Domain.Enum;
using System.Net.NetworkInformation;

namespace Alma.Domain.Entities
{
    public class Campanha
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public float MetaValor { get; set; }
        public float ValorArrecadado { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set;}
        public StatusCampanha StatusCampanha { get; set; }
        public Guid CreatedBy { get; set; }

    }
}
