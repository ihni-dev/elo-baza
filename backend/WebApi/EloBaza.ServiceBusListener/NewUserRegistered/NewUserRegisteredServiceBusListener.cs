using EloBaza.Application.Commands.UserAggregate.Register;
using EloBaza.ServiceBusListener.NewUserRegistered.Configurations;
using MediatR;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EloBaza.ServiceBusListener.NewUserRegistered
{
    class NewUserRegisteredServiceBusListener : IHostedService
    {
        private readonly ILogger<NewUserRegisteredServiceBusListener> _logger;
        private readonly NewUserRegisteredServiceBusConfig _newUserRegisteredServiceBusConfig;
        private readonly ISubscriptionClient _subscriptionClient;
        private readonly IServiceProvider _serviceProvider;

        public NewUserRegisteredServiceBusListener(ILogger<NewUserRegisteredServiceBusListener> logger,
            IOptions<NewUserRegisteredServiceBusConfig> newUserRegisteredServiceBusConfig, 
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            _newUserRegisteredServiceBusConfig = newUserRegisteredServiceBusConfig.Value;
            _subscriptionClient = new SubscriptionClient(_newUserRegisteredServiceBusConfig.ConnectionString,
                _newUserRegisteredServiceBusConfig.TopicName,
                _newUserRegisteredServiceBusConfig.SubscriptionName);
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Starting NewUserRegisteredServiceBusListener");

            RegisterOnMessageHandlerAndReceiveMessages();

            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogDebug($"Stopping NewUserRegisteredServiceBusListener");
            await _subscriptionClient.CloseAsync();
        }

        private void RegisterOnMessageHandlerAndReceiveMessages()
        {
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 3,
                AutoComplete = false
            };

            _subscriptionClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);
        }

        private async Task ProcessMessagesAsync(Message message, CancellationToken cancellationToken)
        {
            var data = Encoding.UTF8.GetString(message.Body);

            _logger.LogInformation($"Received message: SequenceNumber:{message.SystemProperties.SequenceNumber}");
            _logger.LogDebug($"Received message: SequenceNumber:{message.SystemProperties.SequenceNumber} Body:{data}");

            var newUserRegisteredMessage = JsonConvert.DeserializeObject<NewUserRegisteredMessage>(data);

            await _subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);

            using var scope = _serviceProvider.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            try
            {
                await mediator.Send(
                    new RegisterNewUser(newUserRegisteredMessage.UserKey, newUserRegisteredMessage.Email, newUserRegisteredMessage.DisplayName), 
                    cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while handling RegisterNewUser command");
            }
        }

        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            _logger.LogError($"Message handler encountered an exception {exceptionReceivedEventArgs.Exception}.");
            return Task.CompletedTask;
        }
    }
}
