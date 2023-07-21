using BankSystemLibrary.Model;
using BankSystemLibrary.DTO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using BankSystemLibrary.Service;

namespace BankSystemWPF
{
    /// <summary>
    /// Interaction logic for AddEditClientPage.xaml
    /// </summary>
    public partial class AddEditClientPage : Page
    {
        private MainWindow _mainWindow;
        private ClientDTO _currentClient;
        private Service<Client> _service;
        private LogService _logService;
        private NoDepositAccountRefillService _noDepositAccountRefillService;
        private DepositAccountRefillService _depositAccountRefillService;
        private UserNotifications _userNotifications;
        private IChangeClient? _employee;

        private string _currentAction = "add"; // действие, которым вызвана страница

        public AddEditClientPage(Service<Client> service, ClientDTO selectedClient, MainWindow mainWindow, LogService logService, 
            DepositAccountRefillService depositAccountRefillService, NoDepositAccountRefillService noDepositAccountRefillService, UserNotifications userNotifications)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            _service = service;
            _mainWindow = mainWindow;
            _logService = logService;
            _depositAccountRefillService = depositAccountRefillService;
            _noDepositAccountRefillService = noDepositAccountRefillService;
            _userNotifications = userNotifications;
            _employee = BankSystemContext.Employee;

            if (selectedClient != null)
            {
                _currentClient = selectedClient;
                _currentAction = "edit";
                pageHeader.Content = "Редактирование клиента";

                _service.ClientUpdated += _logService.OnEventTriggered;
                _service.ClientUpdated += _userNotifications.ShowNotificationClientUpdated;
            }
            else
            {
                _currentClient = new();
            }

            DataContext = _currentClient; // привязка данных клиента к форме

            #region Проверка прав доступа для редактирования полей
            if (_employee != null && !_employee.CanUpdateLastName())
            {
                lastNameTextBox.IsEnabled = false;
                lastNameTextBox.IsReadOnly = true;
            }
            if (_employee != null && !_employee.CanUpdateFirstName())
            {
                firstNameTextBox.IsEnabled = false;
                firstNameTextBox.IsReadOnly = true;
            }
            if (_employee != null && !_employee.CanUpdateMiddleName())
            {
                middleNameTextBox.IsEnabled = false;
                middleNameTextBox.IsReadOnly = true;
            }
            if (_employee != null && !_employee.CanUpdatePassportNumber())
            {
                passportNumberTextBox.IsEnabled = false;
                passportNumberTextBox.IsReadOnly = true;
            }
            #endregion
        }

        private void SaveClientButton_Click(object sender, RoutedEventArgs e)
        {
            #region Проверка заполнения полей формы

            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrEmpty(_currentClient.LastName))
            {
                errors.AppendLine("Введите фамилию клиента");
            }
            if (string.IsNullOrEmpty(_currentClient.FirstName))
            {
                errors.AppendLine("Введите имя клиента");
            }
            if (string.IsNullOrEmpty(_currentClient.MiddleName))
            {
                errors.AppendLine("Введите отчество клиента");
            }
            if (string.IsNullOrEmpty(_currentClient.PhoneNumber))
            {
                errors.AppendLine("Введите номер телефона клиента");
            }
            if (string.IsNullOrEmpty(_currentClient.PassportNumber))
            {
                errors.AppendLine("Введите номер паспорта клиента");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            #endregion

            if (_currentAction == "add")
            {
                _service.AddClient(_currentClient);
            }
            else if (_currentAction == "edit")
            {
                _service.UpdateClient(_currentClient);
            }

            CancelButton_Click(sender, e);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            _service.ClientUpdated -= _logService.OnEventTriggered;
            _service.ClientUpdated -= _userNotifications.ShowNotificationClientUpdated;

            if (_employee?.GetType() == typeof(Manager))
            {
                _mainWindow.NavigateToPage(new ManagerMainPage(_mainWindow, _logService,
                _depositAccountRefillService, _noDepositAccountRefillService, _service, _userNotifications));
            }
            else if (_employee?.GetType() == typeof(Consultant))
            {
                _mainWindow.NavigateToPage(new ConsultantMainPage(_mainWindow, _logService,
                _depositAccountRefillService, _noDepositAccountRefillService, _service, _userNotifications));
            }
        }
    }
}
