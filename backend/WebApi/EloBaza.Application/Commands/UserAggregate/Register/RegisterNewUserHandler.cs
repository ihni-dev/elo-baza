using EloBaza.Application.IntegrationEvents.UserAggregate;
using EloBaza.Domain.SharedKernel;
using EloBaza.Domain.UserAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EloBaza.Application.Commands.UserAggregate.Register
{
    class RegisterNewUserHandler : AsyncRequestHandler<RegisterNewUser>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IMediator _mediator;

        public RegisterNewUserHandler(IRepository<User> userRepository, IMediator mediator)
        {
            _userRepository = userRepository;
            _mediator = mediator;
        }

        protected override async Task Handle(RegisterNewUser request, CancellationToken cancellationToken)
        {
            var user = User.Create(request.Key, request.Email, request.DisplayName);

            await _userRepository.Save(user, cancellationToken);

            await _mediator.Publish(new NewUserRegistered(user.Email, user.DisplayName));
        }
    }
}
