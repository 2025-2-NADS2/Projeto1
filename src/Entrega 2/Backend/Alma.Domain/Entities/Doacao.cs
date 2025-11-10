using Alma.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alma.Domain.Entities
{
    public class Doacao
    {
        public Guid Id { get; set; }
        public Guid CampanhaId { get; set; }
        public Campanha Campanha { get; set; }
        public Guid UsuarioId { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public StatusDoacao Status { get; set; }
       
    }
}
