using EloBaza.Domain.SharedKernel;
using EloBaza.Domain.SharedKernel.Exceptions;
using EloBaza.Domain.SubjectAggregate;

namespace EloBaza.Application.Commands.SubjectAggregate.ExamSession.Update
{
    public class UpdateExamSessionData
    {
        public short? Year { get; private set; }
        public Semester? Semester { get; private set; }
        public byte? ResitNumber { get; private set; }

        public UpdateExamSessionData(short? year, string? semester, byte? resitNumber)
        {
            using (var validationContext = new ValidationContext())
            {
                if (year.HasValue)
                    validationContext.Validate(
                        () => year < Domain.SubjectAggregate.ExamSession.ExamSessionMinYear || Domain.SubjectAggregate.ExamSession.ExamSessionMaxYear > 2150,
                        nameof(year),
                        $"Year {year} is invalid. Please provide year between 1950 and 2150.");

                if (!(semester is null))
                    validationContext.Validate(
                        () => !Enumeration.HasDisplayName<Semester>(semester),
                        nameof(semester),
                        $"Semester {semester} is invalid.");
            }

            Year = year;
            Semester = semester is null ? null : Enumeration.FromDisplayName<Semester>(semester);
        }
    }
}
