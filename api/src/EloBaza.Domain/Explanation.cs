using System.Collections.Generic;

namespace EloBaza.Domain
{
    public class Explanation
    {
        public int Id { get; private set; }
        public string Content { get; private set; }
        public ICollection<Attachment> Attachments { get; private set; } = new List<Attachment>();

        public Question? Question { get; private set; }

        protected Explanation() { }

        internal Explanation(Question question, string content)
        {
            Question = question;

            Content = content;
        }
    }
}
