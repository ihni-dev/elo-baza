using EloBaza.MailService.Mailing.Config;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EloBaza.MailService.Mailing
{
    class MailService : IMailService
    {
        private readonly SmtpConfig _smtpConfig;

        public MailService(IOptions<SmtpConfig> smtpConfig)
        {
            _smtpConfig = smtpConfig.Value;
        }

        public async Task SendMail(Mail mail, CancellationToken cancellationToken)
        {
            var message = PrepareMessage(mail);
            await Send(message, cancellationToken);
        }

        private MimeMessage PrepareMessage(Mail mail)
        {
            var message = new MimeMessage();

            message.To.AddRange(mail.RecipientsTo.Select(r => MailboxAddress.Parse(r)));
            message.Cc.AddRange(mail.RecipientsCc.Select(r => MailboxAddress.Parse(r)));
            message.Bcc.AddRange(mail.RecipientsBcc.Select(r => MailboxAddress.Parse(r)));
            message.From.Add(new MailboxAddress(_smtpConfig.SenderName, _smtpConfig.SenderEmail));

            message.Subject = mail.Subject;

            message.Body = new TextPart(mail.IsHtml ? "html" : "plain")
            {
                Text = mail.Content
            };

            return message;
        }

        private async Task Send(MimeMessage message, CancellationToken cancellationToken)
        {
            using var client = new SmtpClient();

            await client.ConnectAsync(_smtpConfig.Server, _smtpConfig.Port);

            client.AuthenticationMechanisms.Remove("XOAUTH2");

            await client.AuthenticateAsync(_smtpConfig.SenderEmail, _smtpConfig.SenderPassword);
            await client.SendAsync(message, cancellationToken);
            await client.DisconnectAsync(true, cancellationToken);
        }
    }
}
