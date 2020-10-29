using System.Collections.Generic;

namespace EloBaza.MailService.Mailing
{
    public class Mail
    {
        public IEnumerable<string> RecipientsTo { get; private set; }
        public IEnumerable<string> RecipientsCc { get; private set; }
        public IEnumerable<string> RecipientsBcc { get; private set; }
        public string Content { get; private set; }
        public string Subject { get; private set; }
        public bool IsHtml { get; private set; }

        public Mail(IEnumerable<string> recipientsTo,
            IEnumerable<string> recipientsCc,
            IEnumerable<string> recipientsBcc,
            string content,
            string subject,
            bool isHtml)
        {
            RecipientsTo = recipientsTo;
            RecipientsCc = recipientsCc;
            RecipientsBcc = recipientsBcc;
            Content = content;
            Subject = subject;
            IsHtml = isHtml;
        }
    }
}