using BankSystemWPF.Model;
using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace BankSystemWPF.Pages
{
    /// <summary>
    /// Interaction logic for AddEditClientPage.xaml
    /// </summary>
    public partial class AddEditClientPage : Page
    {
        private MainWindow _mainWindow;
        private ClientDTO _currentClient = new ClientDTO();
        private Service<Client> _service;
        private IChangeClient _employee;

        private string _currentAction = "add"; // действие, которым вызвана страница

        public AddEditClientPage(Service<Client> service, IChangeClient employee, ClientDTO selectedClient, MainWindow mainWindow)
        {
            InitializeComponent();
            this._mainWindow = mainWindow;
            this._service = service;
            this._employee = employee;

            if (selectedClient != null)
            {
                _currentClient = selectedClient;
                _currentAction = "edit";
                pageHeader.Content = "Редактирование клиента";
            }
            
            this.DataContext = _currentClient; // привязка данных клиента к форме

            #region Проверка прав доступа для редактирования полей
            if (!employee.CanUpdateLastName())
            {
                lastNameTextBox.IsEnabled = false;
                lastNameTextBox.IsReadOnly = true;
            }
            if (!employee.CanUpdateFirstName())
            {
                firstNameTextBox.IsEnabled = false;
                firstNameTextBox.IsReadOnly = true;
            }
            if (!employee.CanUpdateMiddleName())
            {
                middleNameTextBox.IsEnabled = false;
                middleNameTextBox.IsReadOnly = true;
            }
            if (!employee.CanUpdatePassportNumber())
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
                _service.AddClient(_currentClient, _employee);
            }
            else if (_currentAction == "edit")
            {
                _service.UpdateClient(_currentClient, _employee);
            }

            this.CancelButton_Click(sender, e);
        }

        private void CancelButton_Click(Object sender, RoutedEventArgs e)
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
