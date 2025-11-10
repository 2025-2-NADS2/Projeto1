

namespace Alma.Application.DTOs.Campanha
{
    public class NovaCampanhaDto
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public decimal MetaValor { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string ImagemUrl { get; set; }
    }
}
