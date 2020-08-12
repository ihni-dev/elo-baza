namespace EloBaza.MailService.Mailing.Config
{
    public class SmtpConfig
    {
        public string Server { get; set; } = default!;
        public int Port { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}