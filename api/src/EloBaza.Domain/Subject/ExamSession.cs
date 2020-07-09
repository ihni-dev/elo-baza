using EloBaza.Domain.SharedKernel;
using System;

namespace EloBaza.Domain.Subject
{
    public class ExamSession : Entity
    {
        public const int ExamSessionNameMaxLength = 70;

        public string Name { get; set; }
        public short Year { get; private set; }
        public Semester Semester { get; private set; }
        public byte? ResitNumber { get; private set; }
        public bool IsResit => ResitNumber.HasValue;

        public SubjectAggregate? Subject { get; private set; }

        protected ExamSession() { }

        internal ExamSession(SubjectAggregate subject, short year, Semester semester)
        {
            Key = Guid.NewGuid();

            Subject = subject;

            Year = year;
            Semester = semester;
            Name = $"{Subject.Name}-{Year}-{Semester}";
        }

        internal void Update(short year, Semester semester)
        {
            Year = year;
            Semester = semester;
        }
    }
}