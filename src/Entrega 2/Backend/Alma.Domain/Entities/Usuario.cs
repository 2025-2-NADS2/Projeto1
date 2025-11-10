using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alma.Domain.Enum;

namespace Alma.Domain.Entities
{
    [Table("usuarios")]
    public class Usuario
    {
        [Key]
        [Column("Id")] // PK char(36)
        [MaxLength(36)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("nome")]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [MaxLength(150)]
        [Column("email")]
        public string Email { get; set; } = string.Empty;

        [MaxLength(20)]
        [Column("telefone")]
        public string? Telefone { get; set; }

        [MaxLength(20)]
        [Column("permissoes")]
        public string? Permissoes { get; set; }

        [MaxLength(10)]
        [Column("status")]
        public string? Status { get; set; }

        [Column("criado_em")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // DEFAULT CURRENT_TIMESTAMP
        public DateTime CriadoEm { get; set; }

        [Column("atualizado_em")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)] // ON UPDATE CURRENT_TIMESTAMP
        public DateTime AtualizadoEm { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("senha_hash")]
        public string SenhaHash { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        [Column("senha")]
        public string Senha { get; set; } = string.Empty; // ATENÇÃO: armazenar senha em texto puro é inseguro. Recomenda-se remover esta coluna ou usá-la apenas para migração.

        [MaxLength(10)]
        [Column("role")]
        private string RoleValor { get; set; } = "user"; // valor persistido

        [NotMapped]
        public RoleUsuario Role
        {
            get => RoleValor == "admin" ? RoleUsuario.Admin : RoleUsuario.User;
            set => RoleValor = value == RoleUsuario.Admin ? "admin" : "user";
        }
    }
}
