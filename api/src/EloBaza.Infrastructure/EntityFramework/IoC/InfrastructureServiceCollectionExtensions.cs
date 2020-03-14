using EloBaza.Application.Contracts;
using EloBaza.Infrastructure.EntityFramework.DbContexts;
using EloBaza.Infrastructure.EntityFramework.Repositories;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Reflection;

namespace EloBaza.Infrastructure.EntityFramework.IoC
{
    public static class InfrastructureServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, string connectionString)
        {
            return services.AddDbContext<SubjectDbContext>(options => options.UseSqlServer(connectionString))
                .AddTransient<IDbConnection>(sp => new SqlConnection(connectionString))
                .AddScoped<ISubjectRepository, SubjectRepository>()
                .AddMediatR(typeof(InfrastructureServiceCollectionExtensions).GetTypeInfo().Assembly);
        }
    }
}
