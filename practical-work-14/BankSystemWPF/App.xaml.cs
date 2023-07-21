using BankSystemLibrary.Model;
using BankSystemLibrary.Repository;
using BankSystemLibrary.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace BankSystemWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider _serviceProvider;

        public App() { }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            if (_serviceProvider is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var services = new ServiceCollection();

            services.AddSingleton<SqliteDataAccess<Account>>();
            services.AddSingleton<LogRepository>();
            services.AddSingleton<SqliteDataAccess<NoDepositAccount>>();
            services.AddSingleton<SqliteDataAccess<DepositAccount>>();
            services.AddSingleton<LogService>();
            services.AddSingleton<NoDepositAccountRefillService>();
            services.AddSingleton<DepositAccountRefillService>();
            services.AddSingleton<Service<Client>>();
            services.AddSingleton<UserNotifications>();
            services.AddSingleton<MainWindow>();

            _serviceProvider = services.BuildServiceProvider();
            var startupForm = _serviceProvider.GetRequiredService<MainWindow>();
            this.MainWindow = startupForm;
            MainWindow.Show();
        }
    }
}
