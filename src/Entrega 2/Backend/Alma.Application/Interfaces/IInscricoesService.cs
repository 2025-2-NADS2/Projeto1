namespace Alma.Application.Interfaces.Repositorios
{
    public interface IInscricoesService
    {
        Task<int> InscreverEvento(int eventoId, string usuarioId);
    }
}
