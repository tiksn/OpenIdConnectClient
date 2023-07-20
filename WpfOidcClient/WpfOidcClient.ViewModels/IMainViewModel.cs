namespace WpfOidcClient.ViewModels;

public interface IMainViewModel
{
    IClientOptionsViewModel ClientOptions { get; }

    ICommandsViewModel Commands { get; }

    IResultsViewModel Results { get; }
}