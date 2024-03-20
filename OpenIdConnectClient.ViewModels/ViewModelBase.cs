using System.Reactive;
using ReactiveUI;
using TIKSN.Concurrency;

namespace OpenIdConnectClient.ViewModels
{
    /// <summary>
    /// A base for all the different view models used throughout the application.
    /// </summary>
    public abstract class ViewModelBase : ReactiveObject, IRoutableViewModel, IActivatableViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelBase"/> class.
        /// </summary>
        /// <param name="urlPathSegments">The title of the view model for routing purposes.</param>
        /// <param name="messageBus"></param>
        /// <param name="schedulers"></param>
        /// <param name="hostScreen">The screen used for routing purposes.</param>
        protected ViewModelBase(
            IEnumerable<string> urlPathSegments,
            IMessageBus messageBus,
            ISchedulers schedulers,
            IScreen hostScreen)
        {
            ArgumentNullException.ThrowIfNull(urlPathSegments);

            if (urlPathSegments.IsEmpty())
            {
                throw new ArgumentOutOfRangeException(nameof(urlPathSegments));
            }

            ArgumentNullException.ThrowIfNull(schedulers);

            UrlPathSegment = string.Join('/', urlPathSegments);
            MessageBus = messageBus ?? throw new ArgumentNullException(nameof(messageBus));
            HostScreen = hostScreen ?? throw new ArgumentNullException(nameof(hostScreen));

            ShowAlert = new Interaction<AlertViewModel, Unit>(schedulers.MainThreadScheduler);
            OpenBrowser = new Interaction<string, Unit>(schedulers.MainThreadScheduler);
        }

        /// <summary>
        /// Gets the current page path.
        /// </summary>
        public string UrlPathSegment { get; }
        public IMessageBus MessageBus { get; }

        /// <summary>
        /// Gets the screen used for routing operations.
        /// </summary>
        public IScreen HostScreen { get; }

        /// <summary>
        /// Gets the activator which contains context information for use in activation of the view model.
        /// </summary>
        public ViewModelActivator Activator { get; } = new ViewModelActivator();

        /// <summary>
        /// Gets a interaction which will show an alert.
        /// </summary>
        public Interaction<AlertViewModel, Unit> ShowAlert { get; }

        /// <summary>
        /// Gets an interaction which will open a browser window.
        /// </summary>
        public Interaction<string, Unit> OpenBrowser { get; }
    }
}
