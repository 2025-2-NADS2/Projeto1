using Alma.Application.Interfaces.Repositorios;
using Alma.Infra.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alma.Infra.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly AlmaDbContext _context;
        private readonly DbSet<T> _db;

        public BaseRepository(AlmaDbContext context)
        {
            _context = context;
            _db = context.Set<T>();
        }
    }
}
