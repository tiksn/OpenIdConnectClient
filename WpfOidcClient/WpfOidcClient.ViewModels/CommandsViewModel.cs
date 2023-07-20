using IdentityModel.OidcClient;
using IdentityModel.OidcClient.Browser;
using IdentityModel.OidcClient.Results;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;

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

        IObservable<string> refreshTokenChanges = messageBus
                    .Changes(x => x.RefreshToken, x => x.RefreshToken);

        _refreshToken = refreshTokenChanges
            .ObserveOn(RxApp.MainThreadScheduler)
            .ToProperty(this, x => x.RefreshToken);

        LogInCommand = ReactiveCommand.CreateFromTask(ExecuteLogInCommandAsync);

        LogOutCommand = ReactiveCommand.Create(() => Unit.Default);

        RefreshCommand = ReactiveCommand.CreateFromTask(
            ExecuteRefreshCommandAsync,
            refreshTokenChanges.Select(x => !string.IsNullOrEmpty(x)));
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

    #region Refresh Command

    public ReactiveCommand<Unit, Unit> RefreshCommand { get; protected set; }

    private async Task<Unit> ExecuteRefreshCommandAsync()
    {
        oidcClientOptions.Browser = browser;

        var _oidcClient = new OidcClient(oidcClientOptions);

        try
        {
            RefreshTokenResult refreshTokenResult = await _oidcClient.RefreshTokenAsync(this.RefreshToken);
            messageBus.SendMessage(refreshTokenResult);
        }
        catch (Exception ex)
        {
            messageBus.SendMessage(new RefreshTokenResult()
            {
                Error = ex.GetType().FullName,
                ErrorDescription = ex.Message,
            });
        }

        return Unit.Default;
    }

    #endregion Refresh Command

    #region Refresh Token property

    private readonly ObservableAsPropertyHelper<string> _refreshToken;

    public string RefreshToken => _refreshToken.Value;

    #endregion Refresh Token property
}