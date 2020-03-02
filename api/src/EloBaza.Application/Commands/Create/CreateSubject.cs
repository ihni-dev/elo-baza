using EloBaza.Domain.SharedKernel;
using MediatR;
using System;

namespace EloBaza.Application.Commands.Create
{
    public class CreateSubject : IRequest<Guid>
    {
        public ICreateSubjectData Data { get; private set; }

        public CreateSubject(ICreateSubjectData data)
        {
            using (var validationContext = new ValidationContext())
            {
                validationContext.Validate(() => string.IsNullOrWhiteSpace(data.Name), nameof(data.Name), "Subject name must be provided");
            }

            Data = data;
        }
    }
}
