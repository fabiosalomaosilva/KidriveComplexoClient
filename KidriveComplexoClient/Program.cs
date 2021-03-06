using Blazored.LocalStorage;
using Blazored.Modal;
using KidriveComplexoClient.Services;
using KidriveComplexoClient.Utils;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using MudBlazor.Services;
using Syncfusion.Blazor;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace KidriveComplexoClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDUwNzIzQDMxMzkyZT1dkTEs0QVYrN283Q009");
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://southa/api/") });
            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5001/kidrivecomplexo/southamerica-east1/api/") });
            builder.Services.AddMudServices(config =>
            {
                config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopRight;
                config.SnackbarConfiguration.PreventDuplicates = false;
                config.SnackbarConfiguration.NewestOnTop = false;
                config.SnackbarConfiguration.ShowCloseIcon = true;
                config.SnackbarConfiguration.VisibleStateDuration = 10000;
                config.SnackbarConfiguration.HideTransitionDuration = 500;
                config.SnackbarConfiguration.ShowTransitionDuration = 500;
                config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
            });
            builder.Services.AddSyncfusionBlazor();

            builder.Services.AddAuthorizationCore();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddBlazoredModal();
            builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthStateProvider>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IOptionsModal, OptionsModal>();
            builder.Services.AddSingleton<IChangeService, ChangeService>();

            await builder.Build().RunAsync();
        }
    }
}
