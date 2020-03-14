using EloBaza.Application.Queries.Subject.GetAll;
using MediatR;

namespace EloBaza.Application.Commands.Subject.Create
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
