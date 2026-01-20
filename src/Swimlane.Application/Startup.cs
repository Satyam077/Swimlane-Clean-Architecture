using Microsoft.Extensions.DependencyInjection;
using Swimlane.Application.IServices;
using Swimlane.Application.Services;

namespace Swimlane.Application
{
    public static class DependencyInject
    {
        public static void InjectApplication(this IServiceCollection service)
        {
            // Register Application Services
            service.AddScoped<ITestService, TestService>();
        }
    }
}
