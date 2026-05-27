using System.Windows;
using KebabGGbab.Localization.Samples.Shared.ViewModels;
using KebabGGbab.Localization.Samples.WPF.Service.Localization;
using Microsoft.Extensions.DependencyInjection;

namespace KebabGGbab.Localization.Samples.WPF
{
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;

        public App()
        {
            ServiceCollection services = new();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddSingleton<MainWindow>();
            services.AddSingleton<MainViewModel>();
            services.AddLocalizationWithResx();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _serviceProvider.GetRequiredService<MainWindow>().Show();
        }
    }
}
