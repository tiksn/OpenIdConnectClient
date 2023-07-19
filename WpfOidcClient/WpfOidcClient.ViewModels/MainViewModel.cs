using ReactiveUI;

namespace WpfOidcClient.ViewModels;

public class MainViewModel : ViewModel, IMainViewModel
{
    public MainViewModel(
        IClientOptionsViewModel clientOptions,
        ICommandsViewModel commands,
        IMessageBus messageBus) : base(messageBus)
    {
        ClientOptions = clientOptions ?? throw new ArgumentNullException(nameof(clientOptions));
        Commands = commands ?? throw new ArgumentNullException(nameof(commands));
    }

    public IClientOptionsViewModel ClientOptions { get; }

    public ICommandsViewModel Commands { get; }
}