namespace EloBaza.MailService.Mailing.Config
{
    public class SmtpConfig
    {
        public string Server { get; set; } = default!;
        public int Port { get; set; } = default!;
        public string SenderName { get; set; } = default!;
        public string SenderEmail { get; set; } = default!;
        public string SenderPassword { get; set; } = default!;
    }
}