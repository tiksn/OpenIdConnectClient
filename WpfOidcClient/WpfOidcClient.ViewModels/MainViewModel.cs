using ReactiveUI;

namespace WpfOidcClient.ViewModels
{
    public class MainViewModel : ViewModel, IMainViewModel
    {
        public MainViewModel(IMessageBus messageBus) : base(messageBus)
        {
        }
    }
}