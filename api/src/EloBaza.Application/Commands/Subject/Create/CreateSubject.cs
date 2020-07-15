using EloBaza.Application.Commands.Common;
using EloBaza.Application.Queries.Subject.Get;
using MediatR;

namespace EloBaza.Application.Commands.Subject.Create
{
    public class CreateSubject : AuditableCommand, IRequest<SubjectDetailsReadModel>
    {
        public CreateSubjectData Data { get; private set; }

        public CreateSubject(int requestorId, CreateSubjectData data) : base(requestorId)
        {
            Data = data;
        }
    }
}
