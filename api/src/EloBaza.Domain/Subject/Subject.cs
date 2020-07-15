using EloBaza.Domain.SharedKernel;
using EloBaza.Domain.SharedKernel.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EloBaza.Domain.SubjectAggregate
{
    public class Subject : AggregateRoot
    {
        public const int NameMaxLength = 50;

        public string Name { get; private set; } = string.Empty;
        public ICollection<ExamSession> ExamSessions { get; private set; } = new List<ExamSession>();
        public ICollection<Category> Categories { get; private set; } = new List<Category>();

        protected Subject() { }

        protected Subject(string name)
        {
            Validate(name);

            Key = Guid.NewGuid();
            Name = name;
        }

        public static Subject Create(int userId, string name)
        {
            var subject = new Subject(name);

            subject.SetCreationData(userId);

            return subject;
        }

        public void UpdateName(int userId, string name)
        {
            Validate(name);

            Name = name;

            SetModificationData(userId);
        }

        public void Delete(int userId)
        {
            MarkAsDeleted(userId);
        }

        #region ExamSession

        public ExamSession CreateExamSession(int userId, short year, Semester semester, byte resitNumber)
        {
            var examSession = ExamSession.Create(userId, this, year, semester, resitNumber);
            if (!(ExamSessions.FirstOrDefault(es => es.Name.Equals(examSession.Name, StringComparison.OrdinalIgnoreCase)) is null))
                throw new AlreadyExistsException($"Exam session: {examSession.Name} already exists");

            ExamSessions.Add(examSession);
            return examSession;
        }

        public void UpdateExamSession(int userId, Guid examSessionKey, short? year, Semester? semester, byte? resitNumber)
        {
            var examSession = FindExamSession(examSessionKey);
            if (examSession is null)
                throw new NotFoundException($"Exam session with Key: {examSessionKey} does not exists");

            var newYear = year ?? examSession.Year;
            var newSemester = semester ?? examSession.Semester;
            var newResitNumber = resitNumber ?? examSession.ResitNumber;

            var hasChanged = examSession.Year != newYear || examSession.Semester != newSemester || examSession.ResitNumber != newResitNumber;
            if (hasChanged)
            {
                if (ExamSessions.Any(es => es.Year == newYear && es.Semester == newSemester && es.ResitNumber == newResitNumber))
                    throw new AlreadyExistsException($"Exam session for year: {year}, semester: {semester} and resit: {resitNumber ?? 0} already exists");
                else
                    examSession.Update(userId, newYear, newSemester, newResitNumber);
            }
        }

        public void DeleteExamSession(int userId, Guid examSessionKey)
        {
            var examSession = FindExamSession(examSessionKey);
            if (examSession is null)
                throw new NotFoundException($"Exam session with Key: {examSessionKey} does not exists for subject: {Key}");

            examSession.MarkAsDeleted(userId);
        }

        #endregion

        private void Validate(string name)
        {
            using var validationContext = new ValidationContext();
            validationContext.Validate(() => string.IsNullOrEmpty(name), nameof(Name), "Subject's name must be provided");
        }

        private ExamSession FindExamSession(Guid examSessionKey)
        {
            var examSession = ExamSessions.SingleOrDefault(es => es.Key == examSessionKey);

            return examSession;
        }
    }
}
