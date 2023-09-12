using ReactiveUI;
using System.Reactive;

namespace OpenIdConnectClient.ViewModels;

public interface IActionsViewModel
{
    public bool AutoRefresh { get; set; }

    ReactiveCommand<Unit, Unit> LogInCommand { get; }

    ReactiveCommand<Unit, Unit> LogOutCommand { get; }

    ReactiveCommand<Unit, Unit> RefreshCommand { get; }
}