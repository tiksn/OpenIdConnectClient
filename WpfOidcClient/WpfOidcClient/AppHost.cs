using IdentityModel.OidcClient.Browser;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using TIKSN.DependencyInjection;
using WpfOidcClient.ViewModels;

namespace WpfOidcClient
{
    public static class AppHost
    {
        private static Lazy<IServiceProvider> serviceProvider
            = new Lazy<IServiceProvider>(CreateServiceProvider, LazyThreadSafetyMode.ExecutionAndPublication);

        public static T GetRequiredService<T>() where T : notnull
        {
            return serviceProvider.Value.GetRequiredService<T>();
        }

        private static IServiceProvider CreateServiceProvider()
        {
            var builder = Host.CreateApplicationBuilder();

            RegisterServices(builder.Services);

            var host = builder.Build();

            return host.Services;
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddFrameworkPlatform();
            services.AddSingleton<IBrowser, WpfEmbeddedBrowser>();

            ViewModelServices.RegisterServices(services);
        }
    }
}