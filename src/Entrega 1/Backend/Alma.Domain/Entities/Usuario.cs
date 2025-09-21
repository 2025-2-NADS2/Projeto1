using System.ComponentModel.DataAnnotations.Schema;
using Alma.Domain.Enum;

namespace Alma.Domain.Entities
{
    public class Usuario
    {
        public Guid Id { get; set; }

        [Column("nome")]
        public string Name { get; set; }

        [Column("senha_hash")]
        public string Senha { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("criado_em")]
        public DateTime DateCreated { get; set; }
    }
}
