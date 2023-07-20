namespace WpfOidcClient.ViewModels;

public interface IResultsViewModel
{
    public string AccessToken { get; }

    public string IdentityToken { get; }

    public string RefreshToken { get; }
}