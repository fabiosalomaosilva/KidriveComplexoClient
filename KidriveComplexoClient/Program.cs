using Blazored.LocalStorage;
using KidriveComplexoClient.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
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

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDUwNzIzQDMxMzkyZTMxMmUzMFRJTnVhTkZFdGdodTJxL0loVFliTzRIOUN5TXRIM1dkTEs0QVYrN283Q009");
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://southamerica-east1-kidrivecomplexo.cloudfunctions.net/api/") });
            builder.Services.AddMudServices();
            builder.Services.AddSyncfusionBlazor();

            builder.Services.AddAuthorizationCore();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthStateProvider>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            await builder.Build().RunAsync();
        }
    }
}
