namespace Alma.Application.DTOs.Usuario
{
    public class NovoUsuarioDto
    {
        public Guid Id { get; set; } // Guid, alinhado ao banco
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Permissoes { get; set; }
        public string Status { get; set; }
        public string Senha { get; set; }
        public string ConfirmarSenha { get; set; }
    }
}
