using Alma.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alma.Application.Interfaces
{
    public interface IOuvidoriaService
    {
        Task EnviarMensagemAsync(Ouvidoria msg);
    }
}
