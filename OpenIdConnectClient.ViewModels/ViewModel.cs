using OpenIdConnectClient.Models;
using ReactiveUI;

namespace OpenIdConnectClient.ViewModels;

public abstract class ViewModel : ViewModelBase
{
    protected ViewModel(
        IEnumerable<string> urlPathSegments,
        IMessageBus messageBus,
        ISchedulers schedulers,
        IScreen hostScreen) : base(urlPathSegments, messageBus, schedulers, hostScreen)
    {
    }
}