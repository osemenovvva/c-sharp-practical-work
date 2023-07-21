using BankSystemLibrary.Model;
using BankSystemLibrary.Service;
using System.Windows;
using System.Windows.Controls;

namespace BankSystemWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private NoDepositAccountRefillService _noDepositAccountRefillService; // Сервис для работы с недепозитными счетами
        private DepositAccountRefillService _depositAccountRefillService; // Сервис для работы с депозитными счетами
        private Service<Client> _service;
        private LogService _logService;
        private UserNotifications _userNotifications;

        public MainWindow(LogService logService, DepositAccountRefillService depositAccountRefillService,
            NoDepositAccountRefillService noDepositAccountRefillService, Service<Client> service, UserNotifications userNotifications)
        {
            InitializeComponent();

            _logService = logService;
            _depositAccountRefillService = depositAccountRefillService;
            _noDepositAccountRefillService = noDepositAccountRefillService;
            _service = service;
            _userNotifications = userNotifications;

            NavigateToPage(new AuthPage(this, _logService, _depositAccountRefillService, _noDepositAccountRefillService, 
                _service, _userNotifications));
        }

        /// <summary>
        /// Метод для перехода на страницу
        /// </summary>
        /// <param name="page">Страница для перехода</param>
        public void NavigateToPage(Page page)
        {
            Content = page;
        }
    }
}
