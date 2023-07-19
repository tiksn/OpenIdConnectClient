using ReactiveUI;

namespace WpfOidcClient.ViewModels;

public class ClientOptionsViewModel : ViewModel, IClientOptionsViewModel
{
    public ClientOptionsViewModel(IMessageBus messageBus) : base(messageBus)
    {
        Authority = "http://localhost:9011/";
        Scope = "openid profile email offline_access";
        RedirectUrl = "http://127.0.0.1/sample-wpf-app";
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

    public string Scope
    {
        get => _scope;
        set => this.RaiseAndSetIfChanged(ref _scope, value);
    }

    #endregion Scope property
}