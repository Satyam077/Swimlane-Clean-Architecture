using Microsoft.AspNetCore.Authentication.Cookies;
using MudBlazor.Services;

namespace Swimlane.UI
{
    public static class DependecyInject
    {

        public static void AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();
            builder.Services.AddMudServices();

            builder.Services.InjectHttpClient(builder.Configuration);
            builder.Services.InjectAuthentication();
            builder.Services.InjectApplicationServices();

        }

        private static void InjectHttpClient(this IServiceCollection services, IConfiguration configuration)
        {
            // services.AddTransient<JwtTokenHandler>();

            services.AddHttpClient("SwimlaneApi", client =>
            {
                var baseUrl = configuration["BaseUrl:ApiUrl"];
                if (!string.IsNullOrEmpty(baseUrl))
                {
                    client.BaseAddress = new Uri(baseUrl);
                }
            });//.AddHttpMessageHandler<JwtTokenHandler>();
        }

        private static void InjectAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.LoginPath = "/login";
            options.LogoutPath = "/logout";
            options.AccessDeniedPath = "/accessdenied";
            options.ExpireTimeSpan = TimeSpan.FromHours(1);
            options.SlidingExpiration = true;
        });

            // Add Authentication services
           // services.AddScoped<TokenValidator>();
        }

        

        private static void InjectApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<Services.TestService>();
        }
    }
}
