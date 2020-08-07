using EloBaza.Application.IntegrationEvents.UserAggregate;
using MailKit.Net.Smtp;
using MediatR;
using MimeKit;
using System.Threading;
using System.Threading.Tasks;

namespace EloBaza.Infrastructure.Mailing.Events.UserAggregate
{
    class NewUserRegisteredHandler : INotificationHandler<NewUserRegistered>
    {
        public async Task Handle(NewUserRegistered notification, CancellationToken cancellationToken)
        {
            var message = new MimeMessage();
            message.To.Add(MailboxAddress.Parse(notification.Email));
            message.From.Add(new MailboxAddress("Bee Keeper", ""));
            message.Subject = "Witamy w StudyBee";

            message.Body = new TextPart("plain")
            {
                Text = $"Witaj {notification.DisplayName}!"
            };

            using var client = new SmtpClient();

            await client.ConnectAsync("smtp.gmail.com", 587);

            client.AuthenticationMechanisms.Remove("XOAUTH2");

            await client.AuthenticateAsync("", "");
            await client.SendAsync(message, cancellationToken);
            await client.DisconnectAsync(true, cancellationToken);
        }
    }
}
