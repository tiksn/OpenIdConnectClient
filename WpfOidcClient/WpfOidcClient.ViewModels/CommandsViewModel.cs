using IdentityModel.OidcClient;
using IdentityModel.OidcClient.Browser;
using ReactiveUI;
using System.Reactive;

namespace WpfOidcClient.ViewModels;

public class CommandsViewModel : ViewModel, ICommandsViewModel
{
    private readonly IBrowser browser;
    private readonly OidcClientOptions oidcClientOptions;

    public CommandsViewModel(
        OidcClientOptions oidcClientOptions,
        IBrowser browser,
        IMessageBus messageBus) : base(messageBus)
    {
        this.oidcClientOptions = oidcClientOptions ?? throw new ArgumentNullException(nameof(oidcClientOptions));
        this.browser = browser ?? throw new ArgumentNullException(nameof(browser));
        LogInCommand = ReactiveCommand.CreateFromTask(ExecuteLogInCommandAsync);
        LogOutCommand = ReactiveCommand.Create(() => Unit.Default);
    }

    #region Log In Command

    public ReactiveCommand<Unit, Unit> LogInCommand { get; protected set; }

    private async Task<Unit> ExecuteLogInCommandAsync()
    {
        oidcClientOptions.Browser = browser;

        var _oidcClient = new OidcClient(oidcClientOptions);

        try
        {
            LoginResult loginResult = await _oidcClient.LoginAsync();
            messageBus.SendMessage(loginResult);
        }
        catch (Exception ex)
        {
            messageBus.SendMessage(new LoginResult()
            {
                Error = ex.GetType().FullName,
                ErrorDescription = ex.Message,
            });
        }

        return Unit.Default;
    }

    #endregion Log In Command

    #region Log Out Command

    public ReactiveCommand<Unit, Unit> LogOutCommand { get; protected set; }

    #endregion Log Out Command
}