using EloBaza.Domain.SharedKernel;
using MediatR;
using System;

namespace EloBaza.Application.Commands.Update
{
    public class UpdateSubject : IRequest
    {
        public Guid Id { get; private set; }
        public UpdateSubjectData Data { get; private set; }

        public UpdateSubject(Guid id, UpdateSubjectData data)
        {
            using (var validationContext = new ValidationContext())
            {
                validationContext.Validate(() => id == default, nameof(Id), "Not empty GUID must be provided");
            }

            Id = id;
            Data = data;
        }
    }
}
