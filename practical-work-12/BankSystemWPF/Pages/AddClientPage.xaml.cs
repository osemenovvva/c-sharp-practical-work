using BankSystemWPF.Model;
using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace BankSystemWPF.Pages
{
    /// <summary>
    /// Interaction logic for AddClientPage.xaml
    /// </summary>
    public partial class AddClientPage : Page
    {
        private MainWindow _mainWindow;
        private Client _client = new Client();
        private Service<Client> _service;

        public AddClientPage(MainWindow mainWindow, Service<Client> service)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            _service = service;
            this.DataContext = _client;
        }

        private void SaveClientButton_Click(object sender, RoutedEventArgs e)
        {
            #region Проверка заполнения полей формы

            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrEmpty(_client.LastName))
            {
                errors.AppendLine("Введите фамилию клиента");
            }
            if (string.IsNullOrEmpty(_client.FirstName))
            {
                errors.AppendLine("Введите имя клиента");
            }
            if (string.IsNullOrEmpty(_client.MiddleName))
            {
                errors.AppendLine("Введите отчество клиента");
            }
            if (string.IsNullOrEmpty(_client.PhoneNumber))
            {
                errors.AppendLine("Введите номер телефона клиента");
            }
            if (string.IsNullOrEmpty(_client.PassportNumber))
            {
                errors.AppendLine("Введите номер паспорта клиента");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            #endregion

            _service.SaveClient(_client);

            this.CancelButton_Click(sender, e);
        }

        private void CancelButton_Click(Object sender, RoutedEventArgs e)
        {
            _mainWindow.NavigateToPage(new ClientsPage(_mainWindow, _service));
        }

    }
}
