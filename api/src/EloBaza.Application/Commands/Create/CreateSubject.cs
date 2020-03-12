using EloBaza.Application.Queries.Subject;
using MediatR;

namespace EloBaza.Application.Commands.Create
{
    public class CreateSubject : IRequest<SubjectReadModel>
    {
        public CreateSubjectData Data { get; private set; }

        public CreateSubject(CreateSubjectData data)
        {
            Data = data;
        }
    }
}
