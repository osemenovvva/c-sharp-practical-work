using BankSystemWPF.Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace BankSystemWPF.Pages
{
    /// <summary>
    /// Interaction logic for ClientsPage.xaml
    /// </summary>
    public partial class ClientsPage : Page
    {
        private MainWindow _mainWindow;
        private Service<Client> _service;

        public ClientsPage(MainWindow mainWindow, Service<Client> service)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            _service = service;
            List<Client> clients = _service.LoadClients();
            dataGrid.ItemsSource = clients;
        }

        private void AddClientButton_Click(Object sender, RoutedEventArgs e)
        {
            _mainWindow.NavigateToPage(new AddClientPage(_mainWindow, _service));
        }

        private void ManageAccountsButton_Click(object sender, RoutedEventArgs e)
        {
            Client selectedObject = (Client)dataGrid.SelectedItem;
            _mainWindow.NavigateToPage(new ClientAccountsPage(_mainWindow, selectedObject, _service));
        }
    }
}
