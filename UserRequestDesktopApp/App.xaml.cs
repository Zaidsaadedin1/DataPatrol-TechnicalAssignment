using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace UserRequestApp
{
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            ServiceProvider = services.BuildServiceProvider();

            var loginWindow = ServiceProvider.GetRequiredService<Login>();
            loginWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient("ApiClient", client =>
            {
                client.BaseAddress = new Uri("http://localhost:5064");
            });

            services.AddTransient<Login>();
            services.AddTransient<RequestWindow>();
            services.AddTransient<SummaryWindow>();

        }
    }
}