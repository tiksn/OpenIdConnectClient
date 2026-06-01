using ReactiveUI;

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
            builder.Services.AddSingleton<IScreen>(screen);

            return builder;
        }

        /// <summary>
        /// The app bootstrap screen is the central location for the RoutingState used for routing between views.
        /// </summary>
        private sealed class AppBootstrapScreen : ReactiveObject, IScreen
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
        public RoutingState Router { get; }
        }
    }
}
