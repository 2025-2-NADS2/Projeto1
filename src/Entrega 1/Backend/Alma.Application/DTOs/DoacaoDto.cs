using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alma.Application.DTOs
{
    public class DoacaoDto
    {
        public Guid CampanhaId { get; set; }
        public decimal Valor { get; set; }
    }
}
