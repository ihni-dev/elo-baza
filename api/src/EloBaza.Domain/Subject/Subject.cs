using EloBaza.Domain.SharedKernel;
using EloBaza.Domain.SharedKernel.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EloBaza.Domain.SubjectAggregate
{
    public class Subject : AggregateRoot
    {
        public const int NameMaxLength = 50;

        public string Name { get; private set; }
        public ICollection<ExamSession> ExamSessions { get; private set; } = new List<ExamSession>();
        public ICollection<Category> Categories { get; private set; } = new List<Category>();

        protected Subject() { }

        public Subject(string name, int userId)
        {
            Validate(name);

            Key = Guid.NewGuid();
            Name = name;
        }

        public ExamSession CreateExamSession(short year, Semester semester, int userId)
        {
            var examSession = new ExamSession(this, year, semester);
            if (!(ExamSessions.FirstOrDefault(es => es.Name.Equals(examSession.Name, StringComparison.OrdinalIgnoreCase)) is null))
                throw new AlreadyExistsException($"Exam session {examSession.Name} already exists");

            ExamSessions.Add(examSession);
            return examSession;
        }

        public void UpdateName(string name)
        {
            Validate(name);

            Name = name;
        }

        public void UpdateExamSession(Guid examSessionKey, short? year, Semester? semester, int userId)
        {
            var examSession = FindExamSession(examSessionKey);
            if (examSession is null)
                throw new NotFoundException($"Exam session with Key: {examSessionKey} does not exists");

            var newYear = year ?? examSession.Year;
            var newSemester = semester ?? examSession.Semester;

            var hasChanged = examSession.Year != newYear || examSession.Semester != newSemester;
            if (hasChanged)
            {
                if (ExamSessions.Any(es => es.Year == newYear && es.Semester == newSemester))
                    throw new AlreadyExistsException($"Exam session for year {year} semester {semester} already exists");
                else
                    examSession.Update(newYear, newSemester);
            }
        }

        public void DeleteExamSession(Guid examSessionKey, int userId)
        {
            var examSession = FindExamSession(examSessionKey);
            if (examSession is null)
                throw new NotFoundException($"Exam session with Key: {examSessionKey} does not exists for subject: {Key}");

            ExamSessions.Remove(examSession);
        }

        private void Validate(string name)
        {
            using var validationContext = new ValidationContext();
            validationContext.Validate(() => string.IsNullOrEmpty(name), nameof(Name), "Subject name must be provided");
        }

        private ExamSession FindExamSession(Guid examSessionKey)
        {
            var examSession = ExamSessions.SingleOrDefault(es => es.Key == examSessionKey);

            return examSession;
        }
    }
}
