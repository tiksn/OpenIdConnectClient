using ReactiveUI;
using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using WpfOidcClient.Models;
using WpfOidcClient.ViewModels;

namespace WpfOidcClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            ViewModel = AppHost.GetRequiredService<IMainViewModel>();

            this.WhenActivated(disposableRegistration =>
            {
                this.Bind(ViewModel,
                    viewModel => viewModel.ClientOptions.Authority,
                    view => view.authorityTextBox.Text)
                    .DisposeWith(disposableRegistration);

                this.Bind(ViewModel,
                    viewModel => viewModel.ClientOptions.ClientId,
                    view => view.clientIdTextBox.Text)
                    .DisposeWith(disposableRegistration);

                this.Bind(ViewModel,
                    viewModel => viewModel.ClientOptions.ClientSecret,
                    view => view.clientSecretTextBox.Text)
                    .DisposeWith(disposableRegistration);

                this.Bind(ViewModel,
                    viewModel => viewModel.ClientOptions.Scope,
                    view => view.scopeTextBox.Text)
                    .DisposeWith(disposableRegistration);

                this.Bind(ViewModel,
                    viewModel => viewModel.ClientOptions.RedirectUrl,
                    view => view.redirectUrlTextBox.Text)
                    .DisposeWith(disposableRegistration);

                this.BindCommand(ViewModel,
                    viewModel => viewModel.Commands.LogInCommand,
                    view => view.logInButton)
                    .DisposeWith(disposableRegistration);

                this.BindCommand(ViewModel,
                    viewModel => viewModel.Commands.RefreshCommand,
                    view => view.refreshButton)
                    .DisposeWith(disposableRegistration);

                this.BindCommand(ViewModel,
                    viewModel => viewModel.Commands.LogOutCommand,
                    view => view.logOutButton)
                    .DisposeWith(disposableRegistration);

                this.OneWayBind(ViewModel,
                    viewModel => viewModel.Results.AccessToken,
                    view => view.accessTokenTextBox.Text)
                    .DisposeWith(disposableRegistration);

                this.OneWayBind(ViewModel,
                    viewModel => viewModel.Results.IdentityToken,
                    view => view.identityTokenTextBox.Text)
                    .DisposeWith(disposableRegistration);

                this.OneWayBind(ViewModel,
                    viewModel => viewModel.Results.RefreshToken,
                    view => view.refreshTokenTextBox.Text)
                    .DisposeWith(disposableRegistration);

                this.OneWayBind(ViewModel,
                    viewModel => viewModel.Results.AccessTokenExpiration,
                    view => view.accessTokenExpirationTextBox.Text)
                    .DisposeWith(disposableRegistration);

                this.OneWayBind(ViewModel,
                    viewModel => viewModel.Results.AccessTokenValidUntil,
                    view => view.accessTokenValidUntilTextBox.Text)
                    .DisposeWith(disposableRegistration);

                this.OneWayBind(ViewModel,
                    viewModel => viewModel.Results.Claims,
                    view => view.claimsDataGrid.ItemsSource)
                    .DisposeWith(disposableRegistration);

                var tickObservable = Observable
                    .Interval(TimeSpan.FromSeconds(1))
                    .Select(_ => new TickModel());

                AppHost
                    .GetRequiredService<IMessageBus>()
                    .RegisterMessageSource(tickObservable);
            });
        }
    }
}