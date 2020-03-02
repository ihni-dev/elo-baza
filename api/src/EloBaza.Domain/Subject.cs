using EloBaza.Domain.SharedKernel;
using System;
using System.Collections.Generic;

namespace EloBaza.Domain
{
    public class Subject
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        //public List<Topic> Topics { get; private set; }
        //public List<ExamSession> ExamSessions { get; private set; }
        //public List<Question> Questions { get; private set; }

        public Subject(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Subject name must be provided");

            Id = Guid.NewGuid();
            Name = name;
            //Topics = new List<Topic>();
            //ExamSessions = new List<ExamSession>();
            //Questions = new List<Question>();
        }

        public void UpdateName(string name)
        {
            using (var validationContext = new ValidationContext())
            {
                validationContext.Validate(() => string.IsNullOrEmpty(name), nameof(Name), "Subject name must be provided");
            }

            Name = name;
        }
    }
}
