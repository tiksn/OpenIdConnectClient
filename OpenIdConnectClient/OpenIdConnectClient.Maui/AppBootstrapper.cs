// Copyright (c) 2019 .NET Foundation and Contributors. All rights reserved.
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using OpenIdConnectClient.Maui.Views;
using OpenIdConnectClient.ViewModels;
using ReactiveUI;
using Splat;

namespace OpenIdConnectClient.Maui
{
    /// <summary>
    /// The app bootstrapper which is used to register everything with the Splat service locator.
    /// </summary>
    public static class AppBootstrapper
    {
        /// <summary>
        /// Registers everything with the Splat service locator.
        /// </summary>
        public static MauiAppBuilder UseAppBootstrapper(this MauiAppBuilder builder)
        {
            var router = new RoutingState();
            var screen = new AppBootstrapScreen(router);
            Locator.CurrentMutable.RegisterConstant(screen, typeof(IScreen));
            builder.Services.AddSingleton<IScreen>(screen);
            Locator.CurrentMutable.Register(() => new AboutView(), typeof(IViewFor<AboutViewModel>));

            return builder;
        }

        /// <summary>
        /// Creates the first main page used within the application.
        /// </summary>
        /// <returns>The page generated.</returns>
        public static Page CreateMainPage()
        {
            // NB: This returns the opening page that the platform-specific
            // boilerplate code will look for. It will know to find us because
            // we've registered our AppBootstrapScreen.
            return new ReactiveUI.Maui.RoutedViewHost();
        }

        /// <summary>
        /// The app bootstrap screen is the central location for the RoutingState used for routing between views.
        /// </summary>
        private class AppBootstrapScreen : ReactiveObject, IScreen
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="AppBootstrapScreen"/> class.
            /// </summary>
            public AppBootstrapScreen(RoutingState router)
            {
                Router = router;
            }

            /// <summary>
            /// Gets or sets the router which is used to navigate between views.
            /// </summary>
            public RoutingState Router { get; protected set; }
        }
    }
}
