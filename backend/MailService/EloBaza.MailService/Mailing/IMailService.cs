using EloBaza.MailService.ServiceBusListener.NewUserRegistered;
using System.Threading;
using System.Threading.Tasks;

namespace EloBaza.MailService.Mailing
{
    interface IMailService
    {
        Task SendWelcomeEmail(NewUserRegisteredMessage newUserRegisteredMessage, CancellationToken cancellationToken);
    }
}
