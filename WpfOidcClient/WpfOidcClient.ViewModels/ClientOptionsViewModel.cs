using IdentityModel.OidcClient;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace WpfOidcClient.ViewModels;

public class ClientOptionsViewModel : ViewModel, IClientOptionsViewModel
{
    public ClientOptionsViewModel(
        OidcClientOptions ovoidClientOptions,
        IMessageBus messageBus) : base(messageBus)
    {
        Authority = "http://localhost:9011/";
        Scope = "openid profile email offline_access";
        RedirectUrl = "http://127.0.0.1/sample-wpf-app";

        oidcClientOptions = ovoidClientOptions ?? throw new ArgumentNullException(nameof(ovoidClientOptions));

        UpdateClientOptions();

        this.WhenAnyValue(x => x.Authority)
            .Select(_ => UpdateClientOptions())
            .Subscribe();

        this.WhenAnyValue(x => x.ClientId)
            .Select(_ => UpdateClientOptions())
            .Subscribe();

        this.WhenAnyValue(x => x.ClientSecret)
            .Select(_ => UpdateClientOptions())
            .Subscribe();

        this.WhenAnyValue(x => x.RedirectUrl)
            .Select(_ => UpdateClientOptions())
            .Subscribe();

        this.WhenAnyValue(x => x.Scope)
            .Select(_ => UpdateClientOptions())
            .Subscribe();
    }

    private Unit UpdateClientOptions()
    {
        oidcClientOptions.Authority = Authority;
        oidcClientOptions.Policy.Discovery.Authority = Authority;
        oidcClientOptions.ClientId = ClientId;
        oidcClientOptions.ClientSecret = ClientSecret;
        oidcClientOptions.RedirectUri = RedirectUrl;
        oidcClientOptions.Scope = Scope;

        return Unit.Default;
    }

    #region Authority property

    private string _authority;

    public string Authority
    {
        get => _authority;
        set => this.RaiseAndSetIfChanged(ref _authority, value);
    }

    #endregion Authority property

    #region ClientId property

    private string _clientId;

    public string ClientId
    {
        get => _clientId;
        set => this.RaiseAndSetIfChanged(ref _clientId, value);
    }

    #endregion ClientId property

    #region ClientSecret property

    private string _clientSecret;

    public string ClientSecret
    {
        get => _clientSecret;
        set => this.RaiseAndSetIfChanged(ref _clientSecret, value);
    }

    #endregion ClientSecret property

    #region RedirectUrl property

    private string _redirectUrl;

    public string RedirectUrl
    {
        get => _redirectUrl;
        set => this.RaiseAndSetIfChanged(ref _redirectUrl, value);
    }

    #endregion RedirectUrl property

    #region Scope property

    private string _scope;
    private readonly OidcClientOptions oidcClientOptions;

    public string Scope
    {
        get => _scope;
        set => this.RaiseAndSetIfChanged(ref _scope, value);
    }

    #endregion Scope property
}