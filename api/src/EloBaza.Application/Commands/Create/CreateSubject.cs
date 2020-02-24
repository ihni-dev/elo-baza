using MediatR;
using System;

namespace EloBaza.Application.Commands.Create
{
    public class CreateSubject : IRequest<Guid>
    {
        public ICreateSubjectData Model { get; private set; }

        public CreateSubject(ICreateSubjectData model)
        {
            Model = model;
        }
    }
}
