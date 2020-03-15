using EloBaza.Domain.SharedKernel;
using MediatR;

namespace EloBaza.Application.Queries.Subject.Get
{
    public class GetSubjectDetails : IRequest<SubjectDetailsReadModel>
    {
        public string Name { get; private set; }

        public GetSubjectDetails(string name)
        {
            using (var validationContext = new ValidationContext())
            {
                validationContext.Validate(() => string.IsNullOrWhiteSpace(name), nameof(name), "Not empty name must be provided");
            }

            Name = name;
        }
    }
}
