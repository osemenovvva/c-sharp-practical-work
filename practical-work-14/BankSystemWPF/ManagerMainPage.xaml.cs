using BankSystemLibrary.Model;
using BankSystemLibrary.DTO;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using BankSystemLibrary.Service;

namespace BankSystemWPF
{
    /// <summary>
    /// Interaction logic for ManagerMainPage.xaml
    /// </summary>
    public partial class ManagerMainPage : Page
    {
        private MainWindow _mainWindow;

        private NoDepositAccountRefillService _noDepositAccountRefillService; // Сервис для работы с недепозитными счетами
        private DepositAccountRefillService _depositAccountRefillService; // Сервис для работы с депозитными счетами
        private Service<Client> _service;
        private LogService _logService;
        private IChangeClient _employee;
        private UserNotifications _notifications;

        public ManagerMainPage(MainWindow mainWindow, LogService logService,  DepositAccountRefillService depositAccountRefillService,
            NoDepositAccountRefillService noDepositAccountRefillService, Service<Client> service, UserNotifications userNotifications)
        {
            InitializeComponent();
            _employee = new Manager();
            BankSystemContext.Employee = _employee;
            BankSystemContext.EmployeeName = _employee.GetType() == typeof(Manager) ? "Менеджер" : "Консультант";

            _mainWindow = mainWindow;
            _logService = logService;
            _depositAccountRefillService = depositAccountRefillService;
            _noDepositAccountRefillService = noDepositAccountRefillService;
            _service = service;
            _notifications = userNotifications;

            List<ClientDTO>? clientsDTO = _service.GetAllClientsView(_employee);
            dataGrid.ItemsSource = clientsDTO;

        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            AuthPage authPage = new(_mainWindow, _logService, _depositAccountRefillService,
                _noDepositAccountRefillService, _service, _notifications);
            _mainWindow.NavigateToPage(authPage);
        }

        private void AddClientButton_Click(object sender, RoutedEventArgs e)
        {
            AddEditClientPage addEditClientPage = new(_service, null, _mainWindow, _logService, 
                _depositAccountRefillService, _noDepositAccountRefillService, _notifications);
            _mainWindow.NavigateToPage(addEditClientPage);
        }

        private void EditClientButton_Click(object sender, RoutedEventArgs e)
        {
            ClientDTO selectedObject = (ClientDTO)dataGrid.SelectedItem;

            AddEditClientPage addEditClientPage = new(_service, selectedObject, _mainWindow, _logService,
                _depositAccountRefillService, _noDepositAccountRefillService, _notifications);
            _mainWindow.NavigateToPage(addEditClientPage);
        }

        private void ManageAccountsButton_Click(object sender, RoutedEventArgs e)
        {
            ClientDTO selectedObject = (ClientDTO)dataGrid.SelectedItem;
            _mainWindow.NavigateToPage(new ClientAccountsPage(_mainWindow, selectedObject, _service, _logService, 
                _depositAccountRefillService, _noDepositAccountRefillService, _notifications));
        }

        private void JournalButton_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.NavigateToPage(new ActionsJournalLog(_mainWindow, _logService, _depositAccountRefillService,
                _noDepositAccountRefillService, _service, _notifications));
        }
    }
}
