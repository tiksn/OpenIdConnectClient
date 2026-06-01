using Autofac;
using Autofac.Extensions.DependencyInjection;
using Duende.IdentityModel.Client;
using Duende.IdentityModel.OidcClient;
using Microsoft.Extensions.Logging;
using OpenIdConnectClient.Models;
using OpenIdConnectClient.ViewModels;
using ReactiveUI;
using Splat.Microsoft.Extensions.DependencyInjection;
using TIKSN.DependencyInjection;
using OidcBrowser = Duende.IdentityModel.OidcClient.Browser.IBrowser;

namespace OpenIdConnectClient.Maui;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseAppBootstrapper()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .ConfigureContainer(new AutofacServiceProviderFactory(), containerBuilder =>
            {
                containerBuilder.RegisterModule<CoreModule>();
                containerBuilder.RegisterModule<PlatformModule>();
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        builder.Services.UseMicrosoftDependencyResolver();
        builder.Services.AddFrameworkPlatform();
        builder.Services.AddSingleton<OidcBrowser, MauiWebAuthenticatorBrowser>();
        builder.Services.AddSingleton(new OidcClientOptions()
        {
            Policy = new Policy
            {
                RequireIdentityTokenSignature = false,
                ValidateTokenIssuerName = false,
                Discovery = new DiscoveryPolicy
                {
                    ValidateIssuerName = false,
                },
            },
        });
        ModelServices.RegisterServices(builder.Services);
        ViewModelServices.RegisterServices(builder.Services);
        ViewServices.RegisterServices(builder.Services);

        var app = builder.Build();

        app.Services.UseMicrosoftDependencyResolver();

        return app;
    }
}
