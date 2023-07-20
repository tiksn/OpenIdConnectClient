using ReactiveUI;
using System.Reactive.Disposables;
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
            });
        }
    }
}