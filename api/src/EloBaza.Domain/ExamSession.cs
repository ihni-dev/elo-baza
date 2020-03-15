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
            using (var validationContext = new ValidationContext())
            {
                validationContext.Validate(() => year < 1950 || year > 2150,
                    nameof(year),
                    $"Year {year} is invalid. Please provide year between 1950 and 2150.");
            }

            Name = CreateName(year, semester);
            Year = year;
            Semester = semester;
            //Questions = new List<Question>();
        }

        internal void Update(int year, Semester semester)
        {
            using (var validationContext = new ValidationContext())
            {
                validationContext.Validate(() => year < 1950 || year > 2150,
                    nameof(year),
                    $"Year {year} is invalid. Please provide year between 1950 and 2150.");
            }

            Name = CreateName(year, semester);
            Year = year;
            Semester = semester;
        }

        private string CreateName(int year, Semester semester) => $"{year}-{semester}";
    }
}