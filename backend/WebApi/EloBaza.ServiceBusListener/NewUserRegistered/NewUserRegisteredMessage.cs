using System;

namespace EloBaza.ServiceBusListener.NewUserRegistered
{
    public class NewUserRegisteredMessage
    {
        public Guid UserKey { get; set; }
        public string Email { get; set; } = default!;
        public string DisplayName { get; set; } = default!;
    }
}
