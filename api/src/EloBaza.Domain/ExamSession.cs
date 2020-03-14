using EloBaza.Domain.SharedKernel;

namespace EloBaza.Domain
{
    public class ExamSession
    {
        public int Id { get; private set; }
        public string Name { get => $"{SubjectName}-{Year}-{Semester}"; }
        public string SubjectName { get; private set; }
        public int Year { get; private set; }
        public Semester Semester { get; private set; }
        //public List<Question> Questions { get; private set; }

        internal ExamSession(string subjectName, int year, Semester semester)
        {
            using (var validationContext = new ValidationContext())
            {
                validationContext.Validate(() => string.IsNullOrWhiteSpace(subjectName), nameof(subjectName), "Subject name must be provided");
                validationContext.Validate(() => year < 1950 || year > 2150,
                    nameof(year),
                    $"Year {year} is invalid. Please provide year between 1950 and 2150.");
            }

            SubjectName = subjectName;
            Year = year;
            Semester = semester;
            //Questions = new List<Question>();
        }

        internal void UpdateSubjectName(string subjectName)
        {
            using (var validationContext = new ValidationContext())
            {
                validationContext.Validate(() => string.IsNullOrWhiteSpace(subjectName), nameof(subjectName), "Subject name must be provided");
            }

            SubjectName = subjectName;
        }
    }
}