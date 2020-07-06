using EloBaza.Domain.SharedKernel;

namespace EloBaza.Domain
{
    public class Answer
    {
        public int Id { get; private set; }
        public string Content { get; private set; }
        public bool IsValid { get; private set; }

        public Question? Question { get; private set; }

        protected Answer() { }

        internal Answer(Question question, string content, bool isValid)
        {
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