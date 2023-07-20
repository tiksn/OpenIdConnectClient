using ReactiveUI;
using System.Reactive;

namespace WpfOidcClient.ViewModels;

public interface ICommandsViewModel
{
    ReactiveCommand<Unit, Unit> LogInCommand { get; }

    ReactiveCommand<Unit, Unit> LogOutCommand { get; }

    ReactiveCommand<Unit, Unit> RefreshCommand { get; }
}