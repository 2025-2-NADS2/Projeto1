using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alma.Domain.Entities
{
    public class Ouvidoria
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string Assunto { get; set; }
        public string Mensagem { get; set; }
        public DateTime DateCreated { get; set; }
        public string? Email { get; set; }

    }
}
