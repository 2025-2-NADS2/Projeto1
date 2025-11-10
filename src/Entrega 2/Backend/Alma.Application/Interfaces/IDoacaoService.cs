using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alma.Application.Interfaces
{
    public interface IDoacaoService
    {
        Task ConfirmarDoacaoAsync(int doacaoId);
        Task<string> CriarSessaoPagamentoAsync(int campanhaId, decimal valor);
    }
}
