using EloBaza.Application.Commands.Common;
using EloBaza.Application.Queries.SubjectAggregate.Get;
using MediatR;

namespace EloBaza.Application.Commands.SubjectAggregate.Create
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
