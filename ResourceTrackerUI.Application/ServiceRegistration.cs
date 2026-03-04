using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using ResourceTrackerUI.Application.Interfaces;
using ResourceTrackerUI.Application.Services;
using ResourceTrackerUI.Application.Services.Auth;

namespace ResourceTrackerUI.Application
{
    public static class ServiceRegistration
    {
        public static void AddAppServices(this IServiceCollection services)
        {
            services.AddScoped<JwtAuthStateProvider>();
            services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<JwtAuthStateProvider>());
            services.AddScoped<AuthHeaderHandler>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ILocalStorageService, LocalStorageService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
        }
    }
}
