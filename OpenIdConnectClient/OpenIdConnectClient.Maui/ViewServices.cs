using OpenIdConnectClient.Maui.Views;
using OpenIdConnectClient.ViewModels;
using ReactiveUI;

namespace OpenIdConnectClient.Maui;

public class ViewServices
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddSingleton<IViewFor<AboutViewModel>, AboutView>();
    }
}
