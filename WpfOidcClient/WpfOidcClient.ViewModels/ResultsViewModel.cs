using IdentityModel.OidcClient;
using ReactiveUI;
using System.Reactive.Linq;

namespace WpfOidcClient.ViewModels;

public class ResultsViewModel : ViewModel, IResultsViewModel
{
    public ResultsViewModel(IMessageBus messageBus) : base(messageBus)
    {
        _accessToken = messageBus
            .Listen<LoginResult>()
            .Select(x => x.AccessToken)
            .ObserveOn(RxApp.MainThreadScheduler)
            .ToProperty(this, x => x.AccessToken);

        _identityToken = messageBus
            .Listen<LoginResult>()
            .Select(x => x.IdentityToken)
            .ObserveOn(RxApp.MainThreadScheduler)
            .ToProperty(this, x => x.IdentityToken);
    }

    #region Access Token property

    private readonly ObservableAsPropertyHelper<string> _accessToken;

    public string AccessToken => _accessToken.Value;

    #endregion Access Token property

    #region Identity Token property

    private readonly ObservableAsPropertyHelper<string> _identityToken;

    public string IdentityToken => _identityToken.Value;

    #endregion Identity Token property
}