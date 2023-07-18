using BankSystemWPF.Model;
using BankSystemWPF.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace BankSystemWPF.View
{
    /// <summary>
    /// Interaction logic for ActionsJournalLog.xaml
    /// </summary>
    public partial class ActionsJournalLog : Page
    {
        private MainWindow _mainWindow;
        private Service<Client> _service;
        private LogService _logService;
        private List<ActionLog> _actionLogs;
        private DepositAccountRefillService _depositAccountRefillService;
        private NoDepositAccountRefillService _noDepositAccountRefillService;
        private UserNotifications _userNotifications;

        public ActionsJournalLog(MainWindow mainWindow, LogService logService, DepositAccountRefillService depositAccountRefillService,
            NoDepositAccountRefillService noDepositAccountRefillService, Service<Client> service, UserNotifications userNotifications)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            _logService = logService;
            _service = service;
            _depositAccountRefillService = depositAccountRefillService;
            _noDepositAccountRefillService = noDepositAccountRefillService;
            _userNotifications = userNotifications;
            _actionLogs = _logService.LoadActionLog();
            dataGrid.ItemsSource = _actionLogs;
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            IChangeClient? employee = BankSystemContext.Employee;

            if (employee?.GetType() == typeof(Manager))
            {
                _mainWindow.NavigateToPage(new ManagerMainPage(_mainWindow, _logService,
                _depositAccountRefillService, _noDepositAccountRefillService, _service, _userNotifications));
            }
            else if (employee?.GetType() == typeof(Consultant))
            {
                _mainWindow.NavigateToPage(new ConsultantMainPage(_mainWindow, _logService,
                _depositAccountRefillService, _noDepositAccountRefillService, _service, _userNotifications));
            }
        }
    }
}
