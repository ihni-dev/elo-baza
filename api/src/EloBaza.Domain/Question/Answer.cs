using EloBaza.Domain.SharedKernel;
using EloBaza.Domain.SharedKernel.Exceptions;
using System;

namespace EloBaza.Domain.Question
{
    public class Answer : Entity
    {
        public string Content { get; private set; }
        public bool IsValid { get; private set; }

        public QuestionAggregate? Question { get; private set; }

        protected Answer() { }

        internal Answer(QuestionAggregate question, string content, bool isValid)
        {
            Key = Guid.NewGuid();

            using (var validationContext = new ValidationContext())
            {
                validationContext.Validate(() => string.IsNullOrEmpty(content), nameof(content), "Answer content must have a value");
            }

            Question = question;

            Content = content;
            IsValid = isValid;
        }
    }
}