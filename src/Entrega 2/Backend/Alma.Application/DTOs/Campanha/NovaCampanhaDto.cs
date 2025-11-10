using Alma.Domain.Enum;

namespace Alma.Application.DTOs.Campanha
{
    public class NovaCampanhaDto
    {
        public int? Id { get; set; }                // usado em update
        public string Titulo { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public decimal? MetaValor { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public StatusCampanha? Status { get; set; } // opcional em update
    }
}
