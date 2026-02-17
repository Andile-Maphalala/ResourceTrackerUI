using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ResourceTrackerUI.ApiClient
{
    public static class ServiceRegistration
    {
        public static void AddResourceTrackerServices(this IServiceCollection services)
        {
            services.AddScoped(sp =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();
                var baseUrl = configuration["ApiBaseUrl"];
                var httpClient = new HttpClient { BaseAddress = new Uri(baseUrl) };
                return new ResourceTrackerApiClient(httpClient);
            });
        }
    }
}
