using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Smokeball.Seo.Ui.Infrastructure;
using System.Windows;

namespace Smokeball.Seo.Ui
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost host;
        private readonly IConfigurationRoot configuration;

        public App()
        {
            configuration = this.GetConfiguration();
            host =  this.GetHost(configuration);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
         
            ShowMainWindow();
        }

        private void ShowMainWindow()
        {
            var mainWindow = host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }
}
