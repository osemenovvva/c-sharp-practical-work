using BankSystemWPF.Model;
using BankSystemWPF.ViewModel;
using System.Windows;
using System.Windows.Controls;


namespace BankSystemWPF.View
{
    /// <summary>
    /// Interaction logic for AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        private MainWindow _mainWindow;
        private LogService _logService;
        private DepositAccountRefillService _depositAccountRefillService;
        private NoDepositAccountRefillService _noDepositAccountRefillService;
        private Service<Client> _service;
        private UserNotifications _userNotifications;

        public AuthPage(MainWindow mainWindow, LogService logService, DepositAccountRefillService depositAccountRefillService,
            NoDepositAccountRefillService noDepositAccountRefillService, Service<Client> service, UserNotifications userNotifications)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            _logService = logService;
            _depositAccountRefillService = depositAccountRefillService;
            _noDepositAccountRefillService = noDepositAccountRefillService;
            _service = service;
            _userNotifications = userNotifications;
        }

        private void ManagerButton_Click(object sender, RoutedEventArgs e)
        {
            ManagerMainPage managerMainPage = new ManagerMainPage(_mainWindow, _logService, _depositAccountRefillService, 
                _noDepositAccountRefillService, _service, _userNotifications);
            _mainWindow.NavigateToPage(managerMainPage);
        }

        private void ConsultantButton_Click(object sender, RoutedEventArgs e)
        {
            ConsultantMainPage consultantMainPage = new ConsultantMainPage(_mainWindow, _logService, 
                _depositAccountRefillService, _noDepositAccountRefillService, _service, _userNotifications);
            _mainWindow.NavigateToPage(consultantMainPage);
        }
    }
}
