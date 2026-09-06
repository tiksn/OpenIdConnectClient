using ReactiveUI;
using TIKSN.Concurrency;

namespace OpenIdConnectClient.ViewModels;

public abstract class ViewModel : ViewModelBase
{
    protected ViewModel(
        IEnumerable<string> urlPathSegments,
        IMessageBus messageBus,
        ISequencers sequencers,
        IScreen hostScreen) : base(urlPathSegments, messageBus, sequencers, hostScreen)
    {
    }
}