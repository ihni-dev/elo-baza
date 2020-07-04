using System.Collections.Generic;

namespace EloBaza.Domain
{
    public class ExamSession
    {
        public int Id { get; private set; }
        public string Name => $"{Subject.Name}-{Year}-{Semester}";
        public int Year { get; private set; }
        public Semester Semester { get; private set; }
        public ICollection<Question> Questions { get; private set; } = new List<Question>();

        public Subject Subject { get; private set; }

        internal ExamSession(Subject subject, int year, Semester semester)
        {
            Subject = subject;

            Year = year;
            Semester = semester;
        }

        internal void Update(int year, Semester semester)
        {
            Year = year;
            Semester = semester;
        }
    }
}