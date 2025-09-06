
using Alma.Domain.Enum;

namespace Alma.Domain.Entities
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public DateTime DateCreted { get; set; }
    }
}
