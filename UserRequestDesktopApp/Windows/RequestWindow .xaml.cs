using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using Cores.Dtos.UserRequests;

namespace UserRequestApp
{
    public partial class RequestWindow : Window
    {
        private readonly HttpClient _httpClient;
        private string _currentUserId;

        public RequestWindow(IHttpClientFactory httpClientFactory)
        {
            InitializeComponent();
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        public void SetUserId(string userId)
        {
            _currentUserId = userId;
        }

        private async void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(txtRequestCode.Text, out var requestCode))
            {
                lblStatus.Text = "Request Code must be a number";
                return;
            }

            var requestDto = new UserRequestCreateDto
            {
                UserId = _currentUserId,
                RequestCode = requestCode,
                Description = txtDescription.Text
            };

            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Request/create", requestDto);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<UserRequestResponseDto>();
                    lblStatus.Text = $"Request created! ID: {result.RequestId}";
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    lblStatus.Text = $"Error: {response.StatusCode} - {error}";
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = $"Error: {ex.Message}";
            }
        }

        private async void BtnViewSummary_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var requestDto = new UserRequestSummaryRequestDto { UserId = _currentUserId };
                var response = await _httpClient.PostAsJsonAsync("api/Request/summary", requestDto);

                if (response.IsSuccessStatusCode)
                {
                    var summary = await response.Content.ReadFromJsonAsync<RequestSummaryDto>();
                    var summaryWindow = new SummaryWindow(summary);
                    summaryWindow.Owner = this;
                    summaryWindow.Show();
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    lblStatus.Text = $"Error: {response.StatusCode} - {error}";
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = $"Error: {ex.Message}";
            }
        }
    }
}