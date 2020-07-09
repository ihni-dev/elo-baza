using EloBaza.Domain.Question;
using EloBaza.Domain.SharedKernel;
using EloBaza.Domain.Subject;
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
            return services
                .AddTransient<IDbConnection>(sp => new SqlConnection(configuration.GetConnectionString("DB")))
                .AddDbContext<SubjectDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DB")))
                .AddDbContext<QuestionDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DB")))
                .AddScoped<IRepository<SubjectAggregate>, SubjectRepository>()
                .AddScoped<IRepository<QuestionAggregate>, QuestionRepository>()
                .AddMediatR(typeof(InfrastructureServiceCollectionExtensions).GetTypeInfo().Assembly);
        }
    }
}
