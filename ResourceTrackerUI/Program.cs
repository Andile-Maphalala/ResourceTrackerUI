using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using ResourceTrackerUI.ApiClient;
using ResourceTrackerUI.Application;
using ResourceTrackerUI.Application.Services.Auth;

namespace ResourceTrackerUI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");


            //////////////////////////////////////////////////
            //mudblazor
            builder.Services.AddMudServices();

            // Register HttpClient for API
            var apiBaseUrl = builder.Configuration["ApiBaseUrl"];
            builder.Services.AddHttpClient("ApiClient", client =>
            {
                client.BaseAddress = new Uri(apiBaseUrl);
            })
            .AddHttpMessageHandler<AuthHeaderHandler>();

            //Services
            builder.Services.AddApiClientServices(apiBaseUrl);
            builder.Services.AddAppServices();

            //Authentication
            builder.Services.AddAuthorizationCore();

            /////////////////////////////////////////////////////
            await builder.Build().RunAsync();
        }
    }
}
