using Microsoft.Extensions.DependencyInjection;

namespace ResourceTrackerUI.ApiClient
{
    public static class ServiceRegistration
    {
        public static void AddApiClientServices(this IServiceCollection services, string baseUrl)
        {
            services.AddScoped(sp =>
            {
                var factory = sp.GetRequiredService<IHttpClientFactory>();
                return new ResourceTrackerApiClient(baseUrl,factory.CreateClient("ApiClient"));
            });
        }
    }
}
