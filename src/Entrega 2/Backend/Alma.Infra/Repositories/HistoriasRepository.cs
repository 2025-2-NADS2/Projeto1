using Alma.Application.Interfaces.Repositorios;
using Alma.Domain.Entities;
using Alma.Infra.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alma.Infra.Repositories
{
    public class HistoriasRepository : BaseRepository<Historias>, IHistoriasRepository
    {
        public HistoriasRepository(AlmaDbContext context) : base(context) { }
    }
}
