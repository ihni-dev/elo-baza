using EloBaza.Domain.SharedKernel.Exceptions;
using EloBaza.Domain.Subject;

namespace EloBaza.Application.Commands.ExamSession.Create
{
    public class CreateExamSessionData
    {
        public short Year { get; private set; }
        public Semester Semester { get; private set; }

        public CreateExamSessionData(short year, Semester semester)
        {
            using (var validationContext = new ValidationContext())
            {
                validationContext.Validate(() => year < 1950 || year > 2150,
                    nameof(year),
                    $"Year {year} is invalid. Please provide year between 1950 and 2150.");
            }

            Year = year;
            Semester = semester;
        }
    }
}
