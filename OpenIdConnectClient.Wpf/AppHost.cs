using Duende.IdentityModel.Client;
using Duende.IdentityModel.OidcClient;
using Duende.IdentityModel.OidcClient.Browser;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenIdConnectClient.Models;
using OpenIdConnectClient.ViewModels;
using ReactiveUI;
using System;
using System.Threading;
using TIKSN.DependencyInjection;

namespace OpenIdConnectClient.Wpf
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
            services.AddFrameworkCore();
            services.AddSingleton<IBrowser, WpfEmbeddedBrowser>();
            services.AddSingleton<IScreen>(new MainScreen(new RoutingState()));

            services.AddSingleton(new OidcClientOptions()
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

            ModelServices.RegisterServices(services);
            ViewModelServices.RegisterServices(services);
        }
    }
}