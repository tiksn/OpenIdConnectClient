using IdentityModel.Client;
using IdentityModel.OidcClient;
using System;
using System.Windows;

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
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var options = new OidcClientOptions()
            {
                Authority = "http://localhost:9011/",
                ClientId = "",
                ClientSecret = "",
                Scope = "openid profile email offline_access",
                RedirectUri = "http://127.0.0.1/sample-wpf-app",
                Browser = new WpfEmbeddedBrowser(),
                Policy = new Policy
                {
                    RequireIdentityTokenSignature = false,
                    ValidateTokenIssuerName = false,
                    Discovery = new DiscoveryPolicy
                    {
                        ValidateIssuerName = false,
                    }
                }
            };

            var _oidcClient = new OidcClient(options);

            LoginResult loginResult;
            try
            {
                loginResult = await _oidcClient.LoginAsync();
            }
            catch (Exception exception)
            {
                txbMessage.Text = $"Unexpected Error: {exception.Message}";
                return;
            }

            if (loginResult.IsError)
            {
                txbMessage.Text = loginResult.Error == "UserCancel" ? "The sign-in window was closed before authorization was completed." : loginResult.Error;
            }
            else
            {
                txbMessage.Text = loginResult.User.Identity.Name;
            }
        }
    }
}
