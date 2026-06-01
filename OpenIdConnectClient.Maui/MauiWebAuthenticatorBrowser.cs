using Duende.IdentityModel.OidcClient.Browser;
#if !WINDOWS
using System.Text;
#endif
using OidcBrowser = Duende.IdentityModel.OidcClient.Browser.IBrowser;

namespace OpenIdConnectClient.Maui;

public class MauiWebAuthenticatorBrowser : OidcBrowser
{
    public async Task<BrowserResult> InvokeAsync(BrowserOptions options, CancellationToken cancellationToken = default)
    {
        try
        {
#if WINDOWS
            return await MainThread.InvokeOnMainThreadAsync(() => InvokeWithWebViewAsync(options, cancellationToken));
#else
            var result = await WebAuthenticator.Default.AuthenticateAsync(
                new WebAuthenticatorOptions
                {
                    Url = new Uri(options.StartUrl),
                    CallbackUrl = new Uri(options.EndUrl)
                },
                cancellationToken);

            return new BrowserResult
            {
                ResultType = BrowserResultType.Success,
                Response = BuildResponse(options.EndUrl, result.Properties)
            };
#endif
        }
        catch (TaskCanceledException)
        {
            return new BrowserResult
            {
                ResultType = BrowserResultType.UserCancel
            };
        }
        catch (Exception ex)
        {
            return new BrowserResult
            {
                ResultType = BrowserResultType.UnknownError,
                Error = ex.GetType().FullName,
                ErrorDescription = ex.Message
            };
        }
    }

#if WINDOWS
    private static async Task<BrowserResult> InvokeWithWebViewAsync(BrowserOptions options, CancellationToken cancellationToken)
    {
        var parentPage = Application.Current?.Windows.FirstOrDefault()?.Page
            ?? throw new InvalidOperationException("The active MAUI window does not contain a page.");

        var completionSource = new TaskCompletionSource<BrowserResult>(TaskCreationOptions.RunContinuationsAsynchronously);

        var webView = new WebView
        {
            Source = options.StartUrl
        };

        webView.Navigating += (_, e) =>
        {
            if (e.Url.StartsWith(options.EndUrl, StringComparison.OrdinalIgnoreCase))
            {
                e.Cancel = true;
                completionSource.TrySetResult(new BrowserResult
                {
                    ResultType = BrowserResultType.Success,
                    Response = e.Url
                });
            }
        };

        var signInPage = new ContentPage
        {
            Title = "Sign In",
            Content = webView
        };

        signInPage.ToolbarItems.Add(new ToolbarItem(
            "Cancel",
            null,
            () => completionSource.TrySetResult(new BrowserResult
            {
                ResultType = BrowserResultType.UserCancel
            })));

        var modalPage = new NavigationPage(signInPage);

        using var cancellationRegistration = cancellationToken.Register(() =>
            completionSource.TrySetResult(new BrowserResult
            {
                ResultType = BrowserResultType.UserCancel
            }));

        await parentPage.Navigation.PushModalAsync(modalPage);

        try
        {
            return await completionSource.Task;
        }
        finally
        {
            if (parentPage.Navigation.ModalStack.Contains(modalPage))
            {
                await parentPage.Navigation.PopModalAsync();
            }
        }
    }
#endif

#if !WINDOWS
    private static string BuildResponse(string endUrl, IReadOnlyDictionary<string, string> properties)
    {
        if (properties.Count == 0)
        {
            return endUrl;
        }

        var builder = new StringBuilder(endUrl);
        builder.Append(endUrl.Contains('?') ? '&' : '?');

        var isFirst = true;
        foreach (var property in properties)
        {
            if (!isFirst)
            {
                builder.Append('&');
            }

            builder
                .Append(Uri.EscapeDataString(property.Key))
                .Append('=')
                .Append(Uri.EscapeDataString(property.Value));

            isFirst = false;
        }

        return builder.ToString();
    }
#endif
}
