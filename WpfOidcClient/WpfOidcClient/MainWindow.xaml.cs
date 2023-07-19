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
            DataContext = ViewModel;

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
            });
        }
    }
}