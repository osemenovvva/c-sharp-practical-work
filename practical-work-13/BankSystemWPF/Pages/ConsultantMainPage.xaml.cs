using BankSystemWPF.Model;
using BankSystemWPF.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace BankSystemWPF
{
    /// <summary>
    /// Interaction logic for ConsultantMainPage.xaml
    /// </summary>
    public partial class ConsultantMainPage : Page
    {
        private MainWindow _mainWindow;
        private SqliteDataAccess<Account> _repository;
        private SqliteDataAccess<NoDepositAccount> _noDepositRepository; // Репозиторий для работы с недепозитными счетами
        private SqliteDataAccess<DepositAccount> _depositRepository; // Репозиторий для работы с депозитными счетами
        private NoDepositAccountRefillService _noDepositAccountRefillService; // Сервис для работы с недепозитными счетами
        private DepositAccountRefillService _depositAccountRefillService; // Сервис для работы с депозитными счетами
        //private ObservableCollection<ActionLog> _logs;
        private Service<Client> _service;
        private LogRepository _logRepository;
        private LogService _logService;
        private IChangeClient _employee;
        private UserNotifications _notifications;

        public ConsultantMainPage(MainWindow mainWindow)
        {
            InitializeComponent();

            this._mainWindow = mainWindow;
            this._repository = new SqliteDataAccess<Account>();
            this._employee = new Consultant();
            _noDepositRepository = new SqliteDataAccess<NoDepositAccount>();
            _depositRepository = new SqliteDataAccess<DepositAccount>();
            _depositAccountRefillService = new DepositAccountRefillService(_depositRepository);
            _noDepositAccountRefillService = new NoDepositAccountRefillService(_noDepositRepository);
            this._service = new Service<Client>(_repository, _noDepositAccountRefillService, _depositAccountRefillService);
            this._logRepository = new LogRepository();
            this._logService = new LogService(_logRepository, _employee);
            _notifications = new UserNotifications();

            List<ClientDTO> clientsDTO = _service.GetAllClientsView(_employee);
            dataGrid.ItemsSource = clientsDTO;

            _service.AccountOpened += _logService.OnEventTriggered;
            _service.AccountClosed += _logService.OnEventTriggered;
            _service.AccountUpdated += _logService.OnEventTriggered;
            _service.MoneyTransfered += _logService.OnEventTriggered;
            _service.ClientUpdated += _logService.OnEventTriggered;
            _depositAccountRefillService.DepositAccountRefilled += _logService.OnEventTriggered;
            _noDepositAccountRefillService.NoDepositAccountRefilled += _logService.OnEventTriggered;
            _service.AccountOpened += _notifications.AccountOpenedNotification;
            _service.AccountClosed += _notifications.AccountClosedNotification;
            _service.AccountUpdated += _notifications.AccountUpdatedNotification;
            _service.MoneyTransfered += _notifications.MoneyTransferedNotification;
            _service.ClientUpdated += _notifications.ClientUpdatedNotification;
            _depositAccountRefillService.DepositAccountRefilled += _notifications.DepositAccountRefilledNotification;
            _noDepositAccountRefillService.NoDepositAccountRefilled += _notifications.NoDepositAccountRefilledNotification;
        }

    private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            AuthPage authPage = new AuthPage(_mainWindow);
            _mainWindow.NavigateToPage(authPage);
        }

        private void EditClientButton_Click(Object sender, RoutedEventArgs e)
        {
            ClientDTO selectedObject = (ClientDTO)dataGrid.SelectedItem;

            AddEditClientPage addEditClientPage = new AddEditClientPage(_service, _employee, selectedObject, _mainWindow);
            _mainWindow.NavigateToPage(addEditClientPage);
        }

        private void ManageAccountsButton_Click(object sender, RoutedEventArgs e)
        {
            ClientDTO selectedObject = (ClientDTO)dataGrid.SelectedItem;
            _mainWindow.NavigateToPage(new ClientAccountsPage(_mainWindow, selectedObject, _service, _employee));
        }

        private void JournalButton_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.NavigateToPage(new ActionsJournalLog(_mainWindow, _employee));
        }

    }
}
