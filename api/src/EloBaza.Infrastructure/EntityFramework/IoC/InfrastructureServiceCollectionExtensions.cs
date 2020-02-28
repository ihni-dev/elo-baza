using EloBaza.Application.Contracts;
using EloBaza.Infrastructure.EntityFramework.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace EloBaza.Infrastructure.EntityFramework.IoC
{
    public static class InfrastructureServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            return services.AddScoped<ISubjectRepository, SubjectRepository>();
        }
    }
}
