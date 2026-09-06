using RxVoid = ReactiveUI.Primitives.RxVoid;
using ReactiveUI;
using System.Reactive;

namespace OpenIdConnectClient.ViewModels;

public interface IActionsViewModel
{
    public bool AutoRefresh { get; set; }

    ReactiveCommand<RxVoid, Unit> LogInCommand { get; }

    ReactiveCommand<RxVoid, Unit> LogOutCommand { get; }

    ReactiveCommand<RxVoid, Unit> RefreshCommand { get; }
}