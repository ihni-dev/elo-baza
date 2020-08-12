using EloBaza.MailService.Mailing.Config;
using EloBaza.MailService.ServiceBusListener.NewUserRegistered;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
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

        public async Task SendWelcomeEmail(NewUserRegisteredMessage newUserRegisteredMessage, CancellationToken cancellationToken)
        {
            var message = new MimeMessage();
            message.To.Add(MailboxAddress.Parse(newUserRegisteredMessage.Email));
            message.From.Add(new MailboxAddress("Bee Keeper", _smtpConfig.Email));
            message.Subject = "Witamy w StudyBee";

            message.Body = new TextPart("plain")
            {
                Text = $"Witaj {newUserRegisteredMessage.DisplayName}!"
            };

            using var client = new SmtpClient();

            await client.ConnectAsync(_smtpConfig.Server, _smtpConfig.Port);

            client.AuthenticationMechanisms.Remove("XOAUTH2");

            await client.AuthenticateAsync(_smtpConfig.Email, _smtpConfig.Password);
            await client.SendAsync(message, cancellationToken);
            await client.DisconnectAsync(true, cancellationToken);
        }
    }
}
