using EloBaza.Domain.SharedKernel;
using System;
using System.Collections.Generic;

namespace EloBaza.Domain.Question
{
    public class Explanation : Entity
    {
        public string Content { get; private set; }
        public ICollection<Attachment> Attachments { get; private set; } = new List<Attachment>();

        public QuestionAggregate? Question { get; private set; }

        protected Explanation() { }

        internal Explanation(QuestionAggregate question, string content)
        {
            Key = Guid.NewGuid();

            Question = question;

            Content = content;
        }
    }
}
