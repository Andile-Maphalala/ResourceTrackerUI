using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using ResourceTrackerUI.Application.Interfaces;
using ResourceTrackerUI.Application.Services.Auth;
using ResourceTrackerUI.Application.Services.Common;
using ResourceTrackerUI.Application.Services.Feature;
using System.Reflection;

namespace ResourceTrackerUI.Application
{
    public static class ServiceRegistration
    {
        public static void AddAppServices(this IServiceCollection services)
        {
            services.AddSingleton<AppCancellationService>();

            services.AddScoped<JwtAuthStateProvider>();
            services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<JwtAuthStateProvider>());
            services.AddScoped<AuthHeaderHandler>();

            RegisterMapper(services);

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ILocalStorageService, LocalStorageService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.AddScoped<IGameService, GameService>();
            services.AddScoped<IComponentService, ComponentService>();
            services.AddScoped<IImportService, ImportService>();
            services.AddScoped<IPictureService, PictureService>();
            services.AddScoped<IGameSaveService, GameSaveService>();
            services.AddScoped<IInventoryService, InventoryService>();
            services.AddScoped<IQuestService, QuestService>();
            services.AddScoped<IBuildPlanService, BuildPlanService>();
            services.AddScoped<IBuildPlanComponetService, BuildPlanComponetService>();
            services.AddScoped<IBuildPlanRequirementService, BuildPlanRequirementService>();

            services.AddScoped<GameSaveState>();

        }


        private static void RegisterMapper(IServiceCollection services)
        {
            var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
            // scans the assembly and gets the IRegister, adding the registration to the TypeAdapterConfig
            typeAdapterConfig.Scan(Assembly.GetExecutingAssembly());
            // register the mapper as Singleton service for my application
            var mapperConfig = new Mapper(typeAdapterConfig);
            services.AddSingleton<IMapper>(mapperConfig);
        }
    }
}
