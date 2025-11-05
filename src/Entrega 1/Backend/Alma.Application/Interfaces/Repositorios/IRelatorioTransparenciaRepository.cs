public interface IRelatorioTransparenciaRepository
{
    Task Adicionar(RelatorioTransparencia relatorio);
    Task<IEnumerable<RelatorioTransparencia>> List();
    Task<RelatorioTransparencia> GetById(Guid id);
}