using EloBaza.Application.Contracts;
using EloBaza.Infrastructure.EntityFramework.DbContexts;
using EloBaza.Infrastructure.EntityFramework.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EloBaza.Infrastructure.EntityFramework.IoC
{
    public static class DbContextServiceCollextionExtensions
    {
        public static IServiceCollection AddDbContexts(this IServiceCollection services, string connectionString)
        {
            return services.AddDbContext<SubjectDbContext>(options => options.UseSqlServer(connectionString))
                .AddScoped<ISubjectRepository, SubjectRepository>;
        }
    }
}
