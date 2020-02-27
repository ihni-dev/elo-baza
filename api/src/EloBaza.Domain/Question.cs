using System;
using System.Collections.Generic;

namespace EloBaza.Domain
{
    public class Question
    {
        public Guid Id { get; private set; }
        public string Content { get; private set; }
        public List<Answer> Answers { get; private set; }
        public List<Annotation> Annotations { get; private set; }
        public bool IsPublished { get; private set; }

        public Question(string content, bool isPublished)
        {
            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentException("Question content must have a value");

            Id = Guid.NewGuid();
            Content = content;
            Answers = new List<Answer>();
            Annotations = new List<Annotation>();
            IsPublished = isPublished;
        }
    }
}