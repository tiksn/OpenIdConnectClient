using IdentityModel.OidcClient;
using IdentityModel.OidcClient.Browser;
using IdentityModel.OidcClient.Results;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;

namespace WpfOidcClient.ViewModels;

public class ActionsViewModel : ViewModel, IActionsViewModel
{
    private readonly IBrowser browser;
    private readonly OidcClientOptions oidcClientOptions;

    public ActionsViewModel(
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

        LogOutCommand = ReactiveCommand.CreateFromTask(
            ExecuteLogOutCommandAsync,
            refreshTokenChanges.Select(x => !string.IsNullOrEmpty(x)));

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

    private async Task<Unit> ExecuteLogOutCommandAsync()
    {
        oidcClientOptions.Browser = browser;

        var _oidcClient = new OidcClient(oidcClientOptions);

        try
        {
            LogoutResult logoutResult = await _oidcClient.LogoutAsync();
            messageBus.SendMessage(logoutResult);
        }
        catch (Exception ex)
        {
            messageBus.SendMessage(new LogoutResult()
            {
                Error = ex.GetType().FullName,
                ErrorDescription = ex.Message,
            });
        }

        return Unit.Default;
    }

    #endregion Log Out Command

    #region Refresh Command

    public ReactiveCommand<Unit, Unit> RefreshCommand { get; protected set; }

    private async Task<Unit> ExecuteRefreshCommandAsync()
    {
        oidcClientOptions.Browser = browser;

        var _oidcClient = new OidcClient(oidcClientOptions);

        try
        {
            RefreshTokenResult refreshTokenResult = await _oidcClient.RefreshTokenAsync(RefreshToken);
            messageBus.SendMessage(refreshTokenResult);

            if (!string.IsNullOrEmpty(refreshTokenResult.AccessToken))
            {
                UserInfoResult userInfoResult = await _oidcClient.GetUserInfoAsync(refreshTokenResult.AccessToken);
                messageBus.SendMessage(userInfoResult);
            }
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

    #region Auto Refresh property

    private bool _autoRefresh;

    public bool AutoRefresh
    {
        get => _autoRefresh;
        set => this.RaiseAndSetIfChanged(ref _autoRefresh, value);
    }

    #endregion Auto Refresh property
}