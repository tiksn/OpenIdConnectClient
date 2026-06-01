using OpenIdConnectClient.Maui.Views;
using OpenIdConnectClient.ViewModels;
using ReactiveUI;

namespace OpenIdConnectClient.Maui;

public static class ViewServices
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddSingleton<MainView>();
        services.AddSingleton<IViewFor<MainViewModel>>(services => services.GetRequiredService<MainView>());
    }
}
