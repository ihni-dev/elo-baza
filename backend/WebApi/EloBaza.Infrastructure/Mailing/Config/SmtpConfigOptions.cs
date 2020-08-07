using Microsoft.Extensions.Options;
namespace EloBaza.Infrastructure.Mailing.Config
{
    public class SmtpConfigOptions : IOptions<SmtpConfig>
    {
        public SmtpConfig Value { get; protected set; } = default!;
    }
}
