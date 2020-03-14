using EloBaza.Domain.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EloBaza.Domain
{
    public class Subject
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public ICollection<ExamSession> ExamSessions { get; private set; }
        //public List<Question> Questions { get; private set; }

        public Subject(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Subject name must be provided");
            }

            Name = name;
            ExamSessions = new List<ExamSession>();
            //Questions = new List<Question>();
        }

        public void CreateExamSession(int year, Semester semester)
        {
            var examSession = new ExamSession(Name, year, semester);
            if (ExamSessions.Any(s => s.Name.Equals(examSession.Name, StringComparison.OrdinalIgnoreCase)))
            {
                throw new AlreadyExistsException($"Session {examSession.Name} already exists");
            }

            ExamSessions.Add(examSession);
        }

        public void UpdateName(string name)
        {
            using (var validationContext = new ValidationContext())
            {
                validationContext.Validate(() => string.IsNullOrEmpty(name), nameof(Name), "Subject name must be provided");
            }

            foreach (var examSession in ExamSessions)
            {
                examSession.UpdateSubjectName(name);
            }

            Name = name;
        }
    }
}
