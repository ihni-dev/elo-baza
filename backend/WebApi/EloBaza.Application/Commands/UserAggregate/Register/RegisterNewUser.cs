using MediatR;
using System;

namespace EloBaza.Application.Commands.UserAggregate.Register
{
    public class RegisterNewUser : IRequest
    {
        public Guid Key { get; private set; }
        public string Email { get; private set; }
        public string DisplayName { get; private set; }

        public RegisterNewUser(Guid key, string email, string displayName)
        {
            Key = key;
            Email = email;
            DisplayName = displayName;
        }
    }
}
