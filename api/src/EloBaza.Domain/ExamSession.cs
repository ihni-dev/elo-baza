using EloBaza.Domain.SharedKernel;

namespace EloBaza.Domain
{
    public class ExamSession
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int Year { get; private set; }
        public Semester Semester { get; private set; }
        //public List<Question> Questions { get; private set; }

        internal ExamSession(int year, Semester semester)
        {
            Name = CreateName(year, semester);
            Year = year;
            Semester = semester;
            //Questions = new List<Question>();
        }

        internal void Update(int year, Semester semester)
        {
            Name = CreateName(year, semester);
            Year = year;
            Semester = semester;
        }

        private static string CreateName(int year, Semester semester) => $"{year}-{semester}";
    }
}