using ReactiveUI;

namespace WpfOidcClient.ViewModels;

public class MainViewModel : ViewModel, IMainViewModel
{
    public MainViewModel(
        IClientOptionsViewModel clientOptions,
        IMessageBus messageBus) : base(messageBus)
    {
        ClientOptions = clientOptions ?? throw new ArgumentNullException(nameof(clientOptions));
    }

    public IClientOptionsViewModel ClientOptions { get; }
}