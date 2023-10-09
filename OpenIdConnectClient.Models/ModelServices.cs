using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;

namespace OpenIdConnectClient.Models;

public static class ModelServices
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddSingleton<ISchedulers>(new Schedulers(RxApp.MainThreadScheduler, RxApp.TaskpoolScheduler));
    }
}