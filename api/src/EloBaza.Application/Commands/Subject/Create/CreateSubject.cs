using EloBaza.Application.Commands.Common;
using EloBaza.Application.Queries.Subject.GetAll;
using MediatR;

namespace EloBaza.Application.Commands.Subject.Create
{
    public class CreateSubject : AuditableCommand, IRequest<SubjectReadModel>
    {
        public CreateSubjectData Data { get; private set; }

        public CreateSubject(CreateSubjectData data, int requestorId) : base(requestorId)
        {
            Data = data;
        }
    }
}
