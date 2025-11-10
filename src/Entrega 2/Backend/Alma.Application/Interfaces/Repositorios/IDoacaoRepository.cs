using Alma.Domain.Entities;

public interface IDoacaoRepository
{
    Task<Doacao> ObterPorId(Guid id);
    Task<IEnumerable<Doacao>> ObterPorCampanha(Guid campanhaId);
    Task Adicionar(Doacao doacao);
    Task Atualizar(Doacao doacao);
}
