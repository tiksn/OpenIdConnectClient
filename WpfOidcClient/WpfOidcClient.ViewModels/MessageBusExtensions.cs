using IdentityModel.OidcClient;
using IdentityModel.OidcClient.Results;
using ReactiveUI;
using System.Reactive.Linq;

namespace WpfOidcClient.ViewModels;

public static class MessageBusExtensions
{
    public static IObservable<T> Changes<T>(
        this IMessageBus messageBus,
        Func<LoginResult, T> loginResultSelector,
        Func<RefreshTokenResult, T> refreshTokenResultSelector)
    {
        if (messageBus is null)
        {
            throw new ArgumentNullException(nameof(messageBus));
        }

        if (loginResultSelector is null)
        {
            throw new ArgumentNullException(nameof(loginResultSelector));
        }

        if (refreshTokenResultSelector is null)
        {
            throw new ArgumentNullException(nameof(refreshTokenResultSelector));
        }

        IObservable<T> loginResultChanges = messageBus
            .Listen<LoginResult>()
            .Select(loginResultSelector);

        IObservable<T> refreshTokenResultChanges = messageBus
            .Listen<RefreshTokenResult>()
            .Select(refreshTokenResultSelector);

        return loginResultChanges.Merge(refreshTokenResultChanges);
    }
}