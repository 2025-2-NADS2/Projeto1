using Alma.Domain.Entities;
using Alma.Infra.Data;
using Microsoft.EntityFrameworkCore;
using System;

public class DoacaoRepository : IDoacaoRepository
{
    private readonly AlmaDbContext _context;

    public DoacaoRepository(AlmaDbContext context)
    {
        _context = context;
    }

    public async Task<Doacao> ObterPorId(Guid id)
    {
        return _context.Doacao.FirstOrDefault(x => x.Id == id);
    }

    public async Task<IEnumerable<Doacao>> ObterPorCampanha(Guid campanhaId)
    {
        return _context.Doacao
            .Where(d => d.CampanhaId == campanhaId)
            .ToList();
    }

    public async Task Adicionar(Doacao doacao)
    {
        _context.Doacao.Add(doacao);
        await _context.SaveChangesAsync();
    }

    public async Task Atualizar(Doacao doacao)
    {
        _context.Doacao.Update(doacao);
        await _context.SaveChangesAsync();
    }
}
