using OpenIdConnectClient.Maui.Views;

namespace OpenIdConnectClient.Maui;

public partial class App : Application
{
    private readonly MainView mainView;

    public App(MainView mainView)
    {
        this.mainView = mainView ?? throw new ArgumentNullException(nameof(mainView));

        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState activationState)
    {
        return new Window(mainView);
    }
}
