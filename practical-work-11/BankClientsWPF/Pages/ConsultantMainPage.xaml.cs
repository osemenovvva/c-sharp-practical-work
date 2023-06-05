using BankClientsWPF.Models;
using System;
using System.Windows;
using System.Windows.Controls;

namespace BankClientsWPF
{
    /// <summary>
    /// Interaction logic for ConsultantMainPage.xaml
    /// </summary>
    public partial class ConsultantMainPage : Page
    {
        private MainWindow _mainWindow;
        private Repository _repository;
        private Service _service;
        private IChangeClient _employee;

        public ConsultantMainPage(MainWindow mainWindow)
        {
            InitializeComponent();

            this._mainWindow = mainWindow;
            this._repository = new Repository();
            this._employee = new Consultant();
            this._service = new Service(_repository);

            ClientDTO[] clientsDTO = _service.GetAllClientsView(_employee);
            dataGrid.ItemsSource = clientsDTO;
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

    }
}
