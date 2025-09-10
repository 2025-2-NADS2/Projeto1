namespace Alma.Application.Interfaces.Repositorios
{
    public interface IInscricoesService
    {
        Task<Guid> InscreverEvento(Guid eventoId, Guid userId);
    }
}
