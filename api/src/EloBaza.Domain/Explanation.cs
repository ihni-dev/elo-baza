using System.Collections.Generic;

namespace EloBaza.Domain
{
    public class Explanation
    {
        public int Id { get; private set; }
        public string Content { get; private set; }
        public ICollection<Attachment> Attachments { get; private set; } = new List<Attachment>();
        
        internal Explanation(string content)
        {
            Content = content;
        }
    }
}
