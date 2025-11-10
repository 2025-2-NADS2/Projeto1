using Alma.Application.Interfaces;
using Alma.Domain.Entities;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System.Threading.Tasks;

namespace Alma.Application.Services
{
    public class OuvidoriaService : IOuvidoriaService
    {
        private readonly IConfiguration _config;

        public OuvidoriaService(IConfiguration config) => _config = config;

        public async Task EnviarMensagemAsync(Ouvidoria msg)
        {
            var emailSettings = _config.GetSection("EmailSettings");
            var remetente = emailSettings["Remetente"];
            var senha = emailSettings["Senha"];
            var destinatario = emailSettings["Destinatario"];
            var servidor = emailSettings["Servidor"];
            var porta = int.Parse(emailSettings["Porta"]);

            var assuntoResumo = msg.Mensagem.Length > 40
                ? msg.Mensagem.Substring(0, 40) + "..."
                : msg.Mensagem;

            var mensagem = new MimeMessage();
            mensagem.From.Add(new MailboxAddress("Ouvidoria do Sistema", remetente));
            mensagem.To.Add(new MailboxAddress("Equipe Administrativa", destinatario));
            mensagem.Subject = $"[Ouvidoria] {assuntoResumo}";

            mensagem.Body = new TextPart("plain")
            {
                Text = $"Mensagem recebida da Ouvidoria:\n\n" +
                       $"Nome: {msg.Nome}\n" +
                       $"Email: {msg.Email}\n" +
                       $"Telefone: {msg.Telefone ?? "(não informado)"}\n\n" +
                       $"Mensagem:\n{msg.Mensagem}"
            };

            using var client = new SmtpClient();
            await client.ConnectAsync(servidor, porta, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(remetente, senha);
            await client.SendAsync(mensagem);
            await client.DisconnectAsync(true);
        }
    }
}
