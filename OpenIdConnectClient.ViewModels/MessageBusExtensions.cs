using Duende.IdentityModel.OidcClient;
using Duende.IdentityModel.OidcClient.Results;
using ReactiveUI;
using System.Reactive.Linq;

namespace OpenIdConnectClient.ViewModels;

public static class MessageBusExtensions
{
    public static IObservable<T> Changes<T>(
        this IMessageBus messageBus,
        Func<LoginResult, T> loginResultSelector,
        Func<RefreshTokenResult, T> refreshTokenResultSelector,
        Func<UserInfoResult, T> userInfoResultSelector)
    {
        if (userInfoResultSelector is null)
        {
            throw new ArgumentNullException(nameof(userInfoResultSelector));
        }

        IObservable<T> userInfoResultChanges = messageBus
            .Listen<UserInfoResult>()
            .Select(userInfoResultSelector);

        return Changes(messageBus, loginResultSelector, refreshTokenResultSelector)
            .Merge(userInfoResultChanges);
    }

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

        IObservable<T> logoutResultChanges = messageBus
            .Listen<LogoutResult>()
            .Select(_ => default(T));

        return loginResultChanges
            .Merge(refreshTokenResultChanges)
            .Merge(logoutResultChanges);
    }
}