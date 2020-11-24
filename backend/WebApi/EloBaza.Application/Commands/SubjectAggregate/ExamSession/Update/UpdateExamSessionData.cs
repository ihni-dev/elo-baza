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
                        () => year < Domain.SubjectAggregate.ExamSession.MinYear || Domain.SubjectAggregate.ExamSession.MaxYear > 2150,
                        nameof(year),
                        $"Year {year} is invalid. Please provide year between 1950 and 2150");

                if (semester is not null)
                    validationContext.Validate(
                        () => !Enumeration.HasDisplayName<Semester>(semester),
                        nameof(semester),
                        $"Semester {semester} is invalid");

                if (resitNumber is not null)
                    validationContext.Validate(
                        () => resitNumber < 0,
                        nameof(semester),
                        $"Resit number must be 0 or higer");
            }

            Year = year;
            Semester = semester is null ? null : Enumeration.FromDisplayName<Semester>(semester);
            ResitNumber = resitNumber;
        }
    }
}
