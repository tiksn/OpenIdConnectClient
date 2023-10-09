﻿using IdentityModel.Client;
using IdentityModel.OidcClient;
using IdentityModel.OidcClient.Browser;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using TIKSN.DependencyInjection;
using OpenIdConnectClient.Models;
using OpenIdConnectClient.ViewModels;

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
            services.AddFrameworkPlatform();
            services.AddSingleton<IBrowser, WpfEmbeddedBrowser>();

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