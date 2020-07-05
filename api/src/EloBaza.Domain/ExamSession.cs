using System;
using System.Collections.Generic;

namespace EloBaza.Domain
{
    public class ExamSession
    {
        public const int ExamSessionNameMaxLength = 70;

        public int Id { get; private set; }
        public string Name { get; set; }
        public short Year { get; private set; }
        public Semester Semester { get; private set; }
        public byte? ResitNumber { get; private set; }
        public bool IsResit => ResitNumber.HasValue;
        public ICollection<Question> Questions { get; private set; } = new List<Question>();

        public Subject Subject { get; private set; }

        protected ExamSession() { }

        internal ExamSession(Subject subject, short year, Semester semester)
        {
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