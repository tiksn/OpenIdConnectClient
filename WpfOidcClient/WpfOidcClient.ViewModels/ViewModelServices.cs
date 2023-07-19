using Microsoft.Extensions.DependencyInjection;

namespace WpfOidcClient.ViewModels;

public static class ViewModelServices
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddSingleton<IMainViewModel, MainViewModel>();
        services.AddSingleton<IClientOptionsViewModel, ClientOptionsViewModel>();
    }
}