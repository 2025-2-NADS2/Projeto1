using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alma.Application.Interfaces.Repositorios;
using Alma.Domain.Entities;
using Alma.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Alma.Infra.Repositories
{
    public class TransparenciaRepository : IRelatorioTransparenciaRepository
    {
        private readonly AlmaDbContext _context;

        public TransparenciaRepository(AlmaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RelatorioTransparencia>> List()
        {
            return await _context.RelatoriosTransparencia
                .OrderByDescending(r => r.DataPublicacao)
                .ToListAsync();
        }

        public async Task Adicionar(RelatorioTransparencia relatorio)
        {
            await _context.RelatoriosTransparencia.AddAsync(relatorio);
            await _context.SaveChangesAsync(); // mantém padrão atual deste repo
        }

        public async Task<RelatorioTransparencia?> GetById(int id)
        {
            return await _context.RelatoriosTransparencia
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}