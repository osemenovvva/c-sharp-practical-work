using BankSystemWPF.Model;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace BankSystemWPF.Pages
{
    /// <summary>
    /// Interaction logic for ActionsJournalLog.xaml
    /// </summary>
    public partial class ActionsJournalLog : Page
    {
        private List<ActionLog> _actionLogs;
        private MainWindow _mainWindow;
        private LogService _logService;
        private IChangeClient _employee;

        public ActionsJournalLog(MainWindow mainWindow, IChangeClient employee, LogService logService)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            _employee = employee;
            _logService = logService;
            _actionLogs = _logService.LoadActionLog();
            dataGrid.ItemsSource = _actionLogs;
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            if (this._employee.GetType() == typeof(Manager))
            {
                _mainWindow.NavigateToPage(new ManagerMainPage(_mainWindow));
            }
            else if (this._employee.GetType() == typeof(Consultant))
            {
                _mainWindow.NavigateToPage(new ConsultantMainPage(_mainWindow));
            }
        }
    }
}
