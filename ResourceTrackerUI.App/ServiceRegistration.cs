using Microsoft.Extensions.DependencyInjection;
using ResourceTrackerUI.App.Interfaces;
using ResourceTrackerUI.App.Services;

namespace ResourceTrackerUI.App
{
    public static class ServiceRegistration
    {
        public static void AddAppServices(this IServiceCollection services)
        {
            services.AddScoped<AuthManagerService>();
            services.AddScoped<IAuthService, AuthService>();
        }
    }
}
