using IdentityModel.Client;
using IdentityModel.OidcClient;
using IdentityModel.OidcClient.Browser;
using ReactiveUI;
using System.Reactive;

namespace WpfOidcClient.ViewModels;

public class CommandsViewModel : ViewModel, ICommandsViewModel
{
    private readonly IBrowser browser;

    public CommandsViewModel(
        IBrowser browser,
        IMessageBus messageBus) : base(messageBus)
    {
        this.browser = browser ?? throw new ArgumentNullException(nameof(browser));
        LogInCommand = ReactiveCommand.CreateFromTask(ExecuteLogInCommandAsync);
        LogOutCommand = ReactiveCommand.Create(() => Unit.Default);
    }

    #region Log In Command

    public ReactiveCommand<Unit, LoginResult> LogInCommand { get; protected set; }

    private async Task<LoginResult> ExecuteLogInCommandAsync()
    {
        var options = new OidcClientOptions()
        {
            Authority = "http://localhost:9011/",
            ClientId = "",
            ClientSecret = "",
            Scope = "openid profile email offline_access",
            RedirectUri = "http://127.0.0.1/sample-wpf-app",
            Browser = browser,
            Policy = new Policy
            {
                RequireIdentityTokenSignature = false,
                ValidateTokenIssuerName = false,
                Discovery = new DiscoveryPolicy
                {
                    ValidateIssuerName = false,
                }
            }
        };

        var _oidcClient = new OidcClient(options);

        return await _oidcClient.LoginAsync();
    }

    #endregion Log In Command

    #region Log Out Command

    public ReactiveCommand<Unit, Unit> LogOutCommand { get; protected set; }

    #endregion Log Out Command
}