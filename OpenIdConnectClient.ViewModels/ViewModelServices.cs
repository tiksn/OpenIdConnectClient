using Microsoft.Extensions.DependencyInjection;

namespace OpenIdConnectClient.ViewModels;

public static class ViewModelServices
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<IMainViewModel>(services => services.GetRequiredService<MainViewModel>());
        services.AddSingleton<IClientOptionsViewModel, ClientOptionsViewModel>();
        services.AddSingleton<IActionsViewModel, ActionsViewModel>();
        services.AddSingleton<IResultsViewModel, ResultsViewModel>();
    }
}
