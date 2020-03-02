using EloBaza.Domain.SharedKernel;
using MediatR;
using System;

namespace EloBaza.Application.Commands.Create
{
    public class CreateSubject : IRequest<Guid>
    {
        public ICreateSubjectData Model { get; private set; }

        public CreateSubject(ICreateSubjectData model)
        {
            using (var validationContext = new ValidationContext())
            {
                validationContext.Validate(() => string.IsNullOrWhiteSpace(model.Name), nameof(model.Name), "Subject name must be provided");
            }

            Model = model;
        }
    }
}
