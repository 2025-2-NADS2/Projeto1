using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alma.Application.Interfaces
{
    public interface IDoacaoService
    {
        Task ConfirmarDoacaoAsync(Guid doacaoId);
        Task<string> CriarSessaoPagamentoAsync(Guid campanhaId, decimal valor);
    }
}
