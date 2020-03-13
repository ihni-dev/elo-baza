using EloBaza.Domain.SharedKernel;
using MediatR;
using System;

namespace EloBaza.Application.Commands.Delete
{
    public class DeleteSubject : IRequest
    {
        public string Name { get; private set; }

        public DeleteSubject(string name)
        {
            using (var validationContext = new ValidationContext())
            {
                validationContext.Validate(() => string.IsNullOrWhiteSpace(name), nameof(name), "Subject name must be provided");
            }

            Name = name;
        }
    }
}
