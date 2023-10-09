namespace OpenIdConnectClient.ViewModels;

public interface IClientOptionsViewModel
{
    string Authority { get; set; }

    string ClientId { get; set; }

    string ClientSecret { get; set; }

    string RedirectUrl { get; set; }

    string Scope { get; set; }
}