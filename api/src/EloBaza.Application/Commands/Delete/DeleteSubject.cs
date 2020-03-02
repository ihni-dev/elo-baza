using EloBaza.Domain.SharedKernel;
using MediatR;
using System;

namespace EloBaza.Application.Commands.Delete
{
    public class DeleteSubject : IRequest
    {
        public Guid Id { get; private set; }

        public DeleteSubject(Guid id)
        {
            using (var validationContext = new ValidationContext())
            {
                validationContext.Validate(() => id == default, nameof(Id), "Not empty GUID must be provided");
            }

            Id = id;
        }
    }
}
