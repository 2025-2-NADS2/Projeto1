using Alma.Infra.Data;
using Microsoft.EntityFrameworkCore;
using System;

public class TransparenciaRepository : IRelatorioTransparenciaRepository
{
    private readonly AlmaDbContext _context;

    public TransparenciaRepository(AlmaDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<RelatorioTransparencia>> List()
    {
        return await _context.RelatorioTransparencia
            .OrderByDescending(r => r.DataPublicacao)
            .ToListAsync();
    }

    public async Task Adicionar(RelatorioTransparencia relatorio)
    {
        await _context.RelatorioTransparencia.AddAsync(relatorio);
        await _context.SaveChangesAsync();
    }

    public async Task<RelatorioTransparencia> GetById(Guid id)
    {
        return _context.RelatorioTransparencia.FirstOrDefault(x => x.Id == id);
    }
}