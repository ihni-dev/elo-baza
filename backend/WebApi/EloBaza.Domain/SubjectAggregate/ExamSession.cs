using EloBaza.Domain.SharedKernel;
using System;

namespace EloBaza.Domain.SubjectAggregate
{
    public class ExamSession : Entity
    {
        public const int ExamSessionNameMaxLength = 70;
        public const short ExamSessionMinYear = 1950;
        public const short ExamSessionMaxYear = 2150;

        public string Name { get; private set; }
        public short Year { get; private set; }
        public Semester Semester { get; private set; }
        public byte ResitNumber { get; private set; }
        public bool IsResit => ResitNumber == default;

        public Subject? Subject { get; private set; }

        protected ExamSession() { }

        protected ExamSession(Subject subject, string name, short year, Semester semester, byte resitNumber = default)
        {
            Key = Guid.NewGuid();

            Subject = subject;

            Name = name;
            Year = year;
            Semester = semester;
            ResitNumber = resitNumber;
        }

        internal static ExamSession Create(int userId, Subject subject, short year, Semester semester, byte resitNumber = default)
        {
            var name = GenerateName(subject, year, semester, resitNumber);
            var examSession = new ExamSession(subject, name, year, semester, resitNumber);

            examSession.SetCreationData(userId);

            return examSession;
        }

        internal void Update(int userId, short year, Semester semester, byte resitNumber)
        {
            Year = year;
            Semester = semester;
            Name = GenerateName(Subject, year, semester, resitNumber);

            SetModificationData(userId);
        }

        private static string GenerateName(Subject? subject, short year, Semester semester, byte resitNumber = default)
        {
            var subjectPrefix = subject is null ? string.Empty : $"{subject.Name}-";
            var resitPostfix = resitNumber != default ? $"-Resit-{ resitNumber}" : string.Empty;
            return $"{subjectPrefix}{year}-{semester}{resitPostfix}";
        }
    }
}