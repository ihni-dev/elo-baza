using System.Threading;
using System.Threading.Tasks;

namespace EloBaza.MailService.Mailing
{
    interface IMailService
    {
        Task SendMail(Mail mail, CancellationToken cancellationToken);
    }
}
