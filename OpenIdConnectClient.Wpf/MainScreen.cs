using ReactiveUI;

namespace OpenIdConnectClient.ViewModels;

public class MainScreen : ReactiveObject, IScreen
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MainScreen"/> class.
    /// </summary>
    public MainScreen(RoutingState router)
    {
        Router = router;
    }

    /// <summary>
    /// Gets or sets the router which is used to navigate between views.
    /// </summary>
    public RoutingState Router { get; protected set; }
}