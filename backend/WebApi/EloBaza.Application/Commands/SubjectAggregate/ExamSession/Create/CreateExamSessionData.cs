using EloBaza.Domain.SharedKernel;
using EloBaza.Domain.SharedKernel.Exceptions;
using EloBaza.Domain.SubjectAggregate;

namespace EloBaza.Application.Commands.SubjectAggregate.ExamSession.Create
{
    public class CreateExamSessionData
    {
        public short Year { get; private set; }
        public Semester Semester { get; private set; }
        public byte ResitNumber { get; private set; }

        public CreateExamSessionData(short year, string semester, byte resitNumber)
        {
            using (var validationContext = new ValidationContext())
            {
                validationContext.Validate(
                    () => year < Domain.SubjectAggregate.ExamSession.MinYear || Domain.SubjectAggregate.ExamSession.MaxYear > 2150,
                    nameof(year),
                    $"Year {year} is invalid. Please provide year between 1950 and 2150.");

                validationContext.Validate(
                    () => !Enumeration.HasDisplayName<Semester>(semester),
                    nameof(semester),
                    $"Semester {semester} is invalid.");
            }

            Year = year;
            Semester = Enumeration.FromDisplayName<Semester>(semester);
            ResitNumber = resitNumber;
        }
    }
}
