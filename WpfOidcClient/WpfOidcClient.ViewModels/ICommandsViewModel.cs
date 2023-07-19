using IdentityModel.OidcClient;
using ReactiveUI;
using System.Reactive;

namespace WpfOidcClient.ViewModels;

public interface ICommandsViewModel
{
    ReactiveCommand<Unit, LoginResult> LogInCommand { get; }

    ReactiveCommand<Unit, Unit> LogOutCommand { get; }
}