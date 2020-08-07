using MediatR;

namespace EloBaza.Application.IntegrationEvents.UserAggregate
{
    public class NewUserRegistered : INotification
    {
        public string Email { get; private set; }
        public string DisplayName { get; private set; }

        public NewUserRegistered(string email, string displayName)
        {
            Email = email;
            DisplayName = displayName;
        }
    }
}
