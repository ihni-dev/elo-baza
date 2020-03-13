using EloBaza.Domain.SharedKernel;
using MediatR;
using System;

namespace EloBaza.Application.Commands.Update
{
    public class UpdateSubject : IRequest
    {
        public string Name { get; private set; }
        public UpdateSubjectData Data { get; private set; }

        public UpdateSubject(string name, UpdateSubjectData data)
        {
            using (var validationContext = new ValidationContext())
            {
                validationContext.Validate(() => string.IsNullOrWhiteSpace(name), nameof(name), "Subject name must be provided");
            }

            Name = name;
            Data = data;
        }
    }
}
