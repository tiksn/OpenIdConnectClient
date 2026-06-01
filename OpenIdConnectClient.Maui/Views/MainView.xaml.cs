using OpenIdConnectClient.Models;
using OpenIdConnectClient.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables.Fluent;
using System.Reactive.Linq;

namespace OpenIdConnectClient.Maui.Views;

public partial class MainView : ContentPageBase<MainViewModel>
{
    public MainView(MainViewModel viewModel)
    {
        InitializeComponent();

        ViewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
        BindingContext = ViewModel;

        logInButton.Command = ViewModel.Actions.LogInCommand;
        refreshButton.Command = ViewModel.Actions.RefreshCommand;
        logOutButton.Command = ViewModel.Actions.LogOutCommand;

        this.WhenActivated(disposables =>
        {
            this.Bind(ViewModel,
                    viewModel => viewModel.ClientOptions.Authority,
                    view => view.authorityEntry.Text)
                .DisposeWith(disposables);

            this.Bind(ViewModel,
                    viewModel => viewModel.ClientOptions.ClientId,
                    view => view.clientIdEntry.Text)
                .DisposeWith(disposables);

            this.Bind(ViewModel,
                    viewModel => viewModel.ClientOptions.ClientSecret,
                    view => view.clientSecretEntry.Text)
                .DisposeWith(disposables);

            this.Bind(ViewModel,
                    viewModel => viewModel.ClientOptions.Scope,
                    view => view.scopeEntry.Text)
                .DisposeWith(disposables);

            this.Bind(ViewModel,
                    viewModel => viewModel.ClientOptions.RedirectUrl,
                    view => view.redirectUrlEntry.Text)
                .DisposeWith(disposables);

            this.Bind(ViewModel,
                    viewModel => viewModel.Actions.AutoRefresh,
                    view => view.autoRefreshSwitch.IsToggled)
                .DisposeWith(disposables);

            this.OneWayBind(ViewModel,
                    viewModel => viewModel.Results.AccessToken,
                    view => view.accessTokenEditor.Text)
                .DisposeWith(disposables);

            this.OneWayBind(ViewModel,
                    viewModel => viewModel.Results.IdentityToken,
                    view => view.identityTokenEditor.Text)
                .DisposeWith(disposables);

            this.OneWayBind(ViewModel,
                    viewModel => viewModel.Results.RefreshToken,
                    view => view.refreshTokenEditor.Text)
                .DisposeWith(disposables);

            this.OneWayBind(ViewModel,
                    viewModel => viewModel.Results.Error,
                    view => view.errorEntry.Text)
                .DisposeWith(disposables);

            this.OneWayBind(ViewModel,
                    viewModel => viewModel.Results.ErrorDescription,
                    view => view.errorDescriptionEditor.Text)
                .DisposeWith(disposables);

            this.OneWayBind(ViewModel,
                    viewModel => viewModel.Results.AccessTokenExpiration,
                    view => view.accessTokenExpirationEntry.Text)
                .DisposeWith(disposables);

            this.OneWayBind(ViewModel,
                    viewModel => viewModel.Results.AccessTokenValidUntil,
                    view => view.accessTokenValidUntilEntry.Text)
                .DisposeWith(disposables);

            this.OneWayBind(ViewModel,
                    viewModel => viewModel.Results.Claims,
                    view => view.claimsCollectionView.ItemsSource)
                .DisposeWith(disposables);

            Observable
                .Interval(TimeSpan.FromSeconds(1))
                .Select(_ => new TickModel())
                .Subscribe(tick => ViewModel.MessageBus.SendMessage(tick))
                .DisposeWith(disposables);
        });
    }
}
