namespace EloBaza.Application.Commands.Common
{
    public class AuditableCommand
    {
        public int RequestorId { get; private set; }

        public AuditableCommand(int requestorId)
        {
            RequestorId = requestorId;
        }
    }
}
