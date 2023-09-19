using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OData;
using OpenIdConnectClient.Models;
using OpenIdConnectClient.ViewModels;
using ReactiveUI;
using TIKSN.DependencyInjection;

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

        builder.Services.AddFrameworkPlatform();
        ModelServices.RegisterServices(builder.Services);
        ViewModelServices.RegisterServices(builder.Services);
        ViewServices.RegisterServices(builder.Services);

        var app = builder.Build();

        app.Services
            .GetRequiredService<IScreen>()
            .Router
            .NavigateAndReset
            .Execute(app.Services.GetRequiredService<AboutViewModel>())
            .Subscribe();

        return app;
    }
}