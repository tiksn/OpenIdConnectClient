using ReactiveUI;

namespace WpfOidcClient.ViewModels;

public class CommandsViewModel : ViewModel, ICommandsViewModel
{
    public CommandsViewModel(IMessageBus messageBus) : base(messageBus)
    {
    }
}
