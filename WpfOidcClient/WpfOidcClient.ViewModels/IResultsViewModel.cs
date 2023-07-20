namespace WpfOidcClient.ViewModels;

public interface IResultsViewModel
{
    public string AccessToken { get; }

    public string AccessTokenExpiration { get; }

    public string IdentityToken { get; }

    public string RefreshToken { get; }
}