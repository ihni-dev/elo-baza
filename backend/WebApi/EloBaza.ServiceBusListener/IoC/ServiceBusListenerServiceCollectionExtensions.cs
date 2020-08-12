using EloBaza.ServiceBusListener.NewUserRegistered;
using EloBaza.ServiceBusListener.NewUserRegistered.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EloBaza.ServiceBusListener.IoC
{
    public static class ServiceBusListenerServiceCollectionExtensions
    {
        public static IServiceCollection AddServiceBusListenerServices(this IServiceCollection services, IConfiguration configuration)
        {
            return services.Configure<NewUserRegisteredServiceBusConfig>(configuration.GetSection("ServiceBus:NotifyNewUserRegistered:WebApi"))
                .AddHostedService<NewUserRegisteredServiceBusListener>();
        }
    }
}
