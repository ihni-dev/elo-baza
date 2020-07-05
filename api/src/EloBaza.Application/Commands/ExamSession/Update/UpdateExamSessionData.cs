using EloBaza.Domain;
using EloBaza.Domain.SharedKernel;

namespace EloBaza.Application.Commands.ExamSession.Update
{
    public class UpdateExamSessionData
    {
        public short? Year { get; private set; }
        public Semester? Semester { get; private set; }

        public UpdateExamSessionData(short? year, Semester? semester)
        {
            using (var validationContext = new ValidationContext())
            {
                if (year.HasValue)
                    validationContext.Validate(() => year < 1950 || year > 2150,
                        nameof(year),
                        $"Year {year} is invalid. Please provide year between 1950 and 2150.");
            }

            Year = year;
            Semester = semester;
        }
    }
}
