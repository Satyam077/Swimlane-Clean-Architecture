using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Swimlane.Core.Abstractions;
using Swimlane.Infrastructure.Repository;
using Swimlane.Infrastructure.Databases;
using Microsoft.EntityFrameworkCore;

namespace Swimlane.Infrastructure
{
    public static class DependencyInject
    {
        public static void InjectInfrastructure(this IServiceCollection service, IConfiguration configuration)
        {
            // Register infrastructure services here
            service.AddDbContext<DatabaseContext>(option => option.UseNpgsql(configuration.GetConnectionString("DbConnection")));
            service.AddScoped<ITestRepository, TestRepository>();
        }

    }
}
