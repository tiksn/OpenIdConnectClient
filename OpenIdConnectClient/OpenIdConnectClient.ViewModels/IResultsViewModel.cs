namespace OpenIdConnectClient.ViewModels;

public interface IResultsViewModel
{
    public string AccessToken { get; }

    public string AccessTokenExpiration { get; }

    public string AccessTokenValidUntil { get; }

    public IReadOnlyList<ClaimViewModel> Claims { get; }

    public string IdentityToken { get; }

    public string RefreshToken { get; }
}