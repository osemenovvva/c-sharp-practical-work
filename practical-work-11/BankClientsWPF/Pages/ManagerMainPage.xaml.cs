using BankClientsWPF.Models;
using System;
using System.Windows;
using System.Windows.Controls;

namespace BankClientsWPF
{
    /// <summary>
    /// Interaction logic for ManagerMainPage.xaml
    /// </summary>
    public partial class ManagerMainPage : Page
    {
        private MainWindow _mainWindow;

        private Repository _repository;
        private Service _service;
        private IChangeClient _employee;

        public ManagerMainPage(MainWindow mainWindow)
        {
            InitializeComponent();

            this._mainWindow = mainWindow;
            this._repository = new Repository();
            this._employee = new Manager();
            this._service = new Service(_repository);

            ClientDTO[] clientsDTO = _service.GetAllClientsView(_employee);
            dataGrid.ItemsSource = clientsDTO;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            AuthPage authPage = new AuthPage(_mainWindow);
            _mainWindow.NavigateToPage(authPage);
        }

        private void AddClientButton_Click(Object sender, RoutedEventArgs e)
        {
            AddEditClientPage addEditClientPage = new AddEditClientPage(_service,  _employee, null, _mainWindow);
            _mainWindow.NavigateToPage(addEditClientPage);
        }

        private void EditClientButton_Click(Object sender, RoutedEventArgs e)
        { 
            ClientDTO selectedObject = (ClientDTO)dataGrid.SelectedItem;

            AddEditClientPage addEditClientPage = new AddEditClientPage(_service, _employee, selectedObject, _mainWindow);
            _mainWindow.NavigateToPage(addEditClientPage);
        }
    }
}
