using EloBaza.Application.Contracts;
using EloBaza.Infrastructure.EntityFramework.DbContexts;
using EloBaza.Infrastructure.EntityFramework.Repositories;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Reflection;

namespace EloBaza.Infrastructure.EntityFramework.IoC
{
    public static class InfrastructureServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddDbContext<SubjectDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DB")))
                .AddTransient<IDbConnection>(sp => new SqlConnection(configuration.GetConnectionString("DB")))
                .AddScoped<ISubjectRepository, SubjectRepository>()
                .AddMediatR(typeof(InfrastructureServiceCollectionExtensions).GetTypeInfo().Assembly);
        }
    }
}
