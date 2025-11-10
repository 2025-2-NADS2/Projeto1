using Alma.Application.Interfaces.Repositorios;
using Alma.Infra.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alma.Infra.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AlmaDbContext _context;

        public UnitOfWork(AlmaDbContext context)
        {
            _context = context;
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
