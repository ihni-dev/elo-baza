using EloBaza.Application.Commands;
using EloBaza.Application.Commands.Create;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace EloBaza.Application.IoC
{
    public static class ApplicationServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            return services.AddMediatR(typeof(ApplicationServiceCollectionExtension).GetType().Assembly);
        }
    }
}
