using OpenIdConnectClient.Models;
using ReactiveUI;
using static LanguageExt.Prelude;

namespace OpenIdConnectClient.ViewModels;

public class MainViewModel : ViewModel, IMainViewModel
{
    public MainViewModel(
        IClientOptionsViewModel clientOptions,
        IActionsViewModel actions,
        IResultsViewModel results,
        IMessageBus messageBus,
        ISchedulers schedulers,
        IScreen hostScreen)
        : base(Seq1("Main"), messageBus, schedulers, hostScreen)
    {
        ClientOptions = clientOptions ?? throw new ArgumentNullException(nameof(clientOptions));
        Actions = actions ?? throw new ArgumentNullException(nameof(actions));
        Results = results ?? throw new ArgumentNullException(nameof(results));
    }

    public IClientOptionsViewModel ClientOptions { get; }

    public IActionsViewModel Actions { get; }

    public IResultsViewModel Results { get; }
}