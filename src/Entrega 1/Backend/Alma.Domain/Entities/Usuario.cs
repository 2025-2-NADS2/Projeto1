using System.ComponentModel.DataAnnotations.Schema;
using Alma.Domain.Enum;

namespace Alma.Domain.Entities
{
    [Table("usuarios")]
    public class Usuario
    {
        [Column("Id")] // coluna no banco é "Id" (char(36))
        public Guid Id { get; set; }

        [Column("nome")]
        public string Nome { get; set; } // NOT NULL

        [Column("email")]
        public string Email { get; set; } // NOT NULL

        [Column("telefone")]
        public string? Telefone { get; set; } // NULLABLE no banco

        [Column("permissoes")]
        public string? Permissoes { get; set; } // NULLABLE no banco

        [Column("status")]
        public string? Status { get; set; } // NULLABLE no banco

        [Column("criado_em")]
        public DateTime CriadoEm { get; set; } // NOT NULL

        [Column("atualizado_em")]
        public DateTime AtualizadoEm { get; set; } // NOT NULL

        [Column("senha_hash")]
        public string SenhaHash { get; set; } // NOT NULL
    }
}
