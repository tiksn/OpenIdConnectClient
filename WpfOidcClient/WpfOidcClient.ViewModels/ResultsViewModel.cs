using ReactiveUI;

namespace WpfOidcClient.ViewModels;

public class ResultsViewModel : ViewModel, IResultsViewModel
{
    public ResultsViewModel(IMessageBus messageBus) : base(messageBus)
    {
    }
}
