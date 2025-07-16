using System.Net.Http;
using System.Windows;
using Newtonsoft.Json;
using Cores.Dtos.UserInfo;
using Microsoft.Extensions.DependencyInjection;

namespace UserRequestApp
{
    public partial class Login : Window
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IServiceProvider _serviceProvider;

        public Login(IHttpClientFactory httpClientFactory, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _httpClientFactory = httpClientFactory;
            _serviceProvider = serviceProvider;
        }

        private async void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            var username = txtUsername.Text.Trim();

            if (string.IsNullOrWhiteSpace(username))
            {
                lblStatus.Text = "Username is required";
                return;
            }

            var client = _httpClientFactory.CreateClient("ApiClient");
            var response = await client.PostAsJsonAsync("user/reg", new { username });

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<RegistrationResponseDto>(content);

                var requestWindow = _serviceProvider.GetRequiredService<RequestWindow>();
                requestWindow.SetUserId(result.UserId);
                requestWindow.Show();
                this.Close();
            }
            else
            {
                lblStatus.Text = "Login failed. Please try again.";
            }

        }
    }
}