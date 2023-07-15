using BankSystemWPF.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
        private LogRepository _logRepository;
        private IChangeClient _employee;

        public ActionsJournalLog(MainWindow mainWindow, IChangeClient employee)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            _employee = employee;
            _logRepository= new LogRepository();
            _logService = new LogService(_logRepository, _employee  );
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
