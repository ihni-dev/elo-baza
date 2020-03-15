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
                throw new ArgumentException("Subject name must be provided");

            Name = name;
            ExamSessions = new List<ExamSession>();
            //Questions = new List<Question>();
        }

        public ExamSession CreateExamSession(int year, Semester semester)
        {
            var examSession = new ExamSession(year, semester);
            if (!(FindExamSession(examSession.Name) is null))
                throw new AlreadyExistsException($"Exam session {examSession.Name} already exists");

            ExamSessions.Add(examSession);
            return examSession;
        }

        public void UpdateName(string name)
        {
            using (var validationContext = new ValidationContext())
            {
                validationContext.Validate(() => string.IsNullOrEmpty(name), nameof(Name), "Subject name must be provided");
            }

            Name = name;
        }

        public void UpdateExamSession(string name, int? year, Semester? semester)
        {
            var examSession = FindExamSession(name);
            if (examSession is null)
                throw new NotFoundException($"Exam session with Name: {name} does not exists");

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

        public void DeleteExamSession(string name)
        {
            var examSession = FindExamSession(name);
            if (examSession is null)
                throw new NotFoundException($"Exam session with Name: {name} does not exists");

            ExamSessions.Remove(examSession);
        }

        private ExamSession FindExamSession(string name)
        {
            var examSession = ExamSessions.SingleOrDefault(es => es.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            return examSession;
        }
    }
}
