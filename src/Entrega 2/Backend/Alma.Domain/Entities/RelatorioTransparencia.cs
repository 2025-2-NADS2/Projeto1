public class RelatorioTransparencia
{
    public Guid Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string CaminhoArquivo { get; set; } = string.Empty; // Ex: /uploads/transparencia/relatorio_julho.pdf
    public DateTime DataPublicacao { get; set; } = DateTime.UtcNow;
}
