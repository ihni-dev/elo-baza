using EloBaza.Domain.SharedKernel;
using System;

namespace EloBaza.Domain.SubjectAggregate
{
    public class ExamSession : Entity
    {
        public const int ExamSessionNameMaxLength = 70;
        public const short ExamSessionMinYear = 1950;
        public const short ExamSessionMaxYear = 2150;

        public string Name { get; set; }
        public short Year { get; private set; }
        public Semester Semester { get; private set; }
        public byte? ResitNumber { get; private set; }
        public bool IsResit => ResitNumber.HasValue;

        public Subject? Subject { get; private set; }

        protected ExamSession() { }

        internal ExamSession(Subject subject, short year, Semester semester)
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