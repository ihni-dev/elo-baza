namespace EloBaza.Infrastructure.Mailing.Config
{
    public class SmtpConfig
    {
        public string Server { get; protected set; } = default!;
        public int Port { get; protected set; } = default!;
        public string Email { get; protected set; } = default!;
        public string Password { get; protected set; } = default!;
    }
}