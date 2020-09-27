using EloBaza.Domain.SharedKernel.Exceptions;

namespace EloBaza.Application.Commands.SubjectAggregate.Update
{
    public class UpdateSubjectData
    {
        public string Name { get; private set; }

        public UpdateSubjectData(string name)
        {
            using (var validationContext = new ValidationContext())
            {
                validationContext.Validate(() => string.IsNullOrWhiteSpace(name), nameof(name), "Subject name must be provided");
            }

            Name = name;
        }
    }
}
