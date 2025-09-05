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
    public class InscricoesRepository : BaseRepository<Inscricoes>, IInscricoesRepository
    {
        public InscricoesRepository(AlmaDbContext context) : base(context) { }
    }
}
