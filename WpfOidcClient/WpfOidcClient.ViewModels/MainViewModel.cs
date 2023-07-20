using ReactiveUI;

namespace WpfOidcClient.ViewModels;

public class MainViewModel : ViewModel, IMainViewModel
{
    public MainViewModel(
        IClientOptionsViewModel clientOptions,
        IActionsViewModel actions,
        IResultsViewModel results,
        IMessageBus messageBus) : base(messageBus)
    {
        ClientOptions = clientOptions ?? throw new ArgumentNullException(nameof(clientOptions));
        Actions = actions ?? throw new ArgumentNullException(nameof(actions));
        Results = results ?? throw new ArgumentNullException(nameof(results));
    }

    public IClientOptionsViewModel ClientOptions { get; }

    public IActionsViewModel Actions { get; }

    public IResultsViewModel Results { get; }
}