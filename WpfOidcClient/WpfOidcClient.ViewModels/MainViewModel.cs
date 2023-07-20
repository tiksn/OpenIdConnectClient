using ReactiveUI;

namespace WpfOidcClient.ViewModels;

public class MainViewModel : ViewModel, IMainViewModel
{
    public MainViewModel(
        IClientOptionsViewModel clientOptions,
        ICommandsViewModel commands,
        IResultsViewModel results,
        IMessageBus messageBus) : base(messageBus)
    {
        ClientOptions = clientOptions ?? throw new ArgumentNullException(nameof(clientOptions));
        Commands = commands ?? throw new ArgumentNullException(nameof(commands));
        Results = results ?? throw new ArgumentNullException(nameof(results));
    }

    public IClientOptionsViewModel ClientOptions { get; }

    public ICommandsViewModel Commands { get; }

    public IResultsViewModel Results { get; }
}