using BankSystemWPF.Model;
using BankSystemWPF.Pages;
using System.Windows;
using System.Windows.Controls;

namespace BankSystemWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Service<Client> _service;
        private SqliteDataAccess<Account> _repository;

        public MainWindow()
        {
            InitializeComponent();
            _repository = new SqliteDataAccess<Account>();
            _service = new Service<Client>(_repository);
            NavigateToPage(new ClientsPage(this, _service));
        }

        /// <summary>
        /// Метод для перехода на страницу
        /// </summary>
        /// <param name="page">Страница для перехода</param>
        public void NavigateToPage(Page page)
        {
            this.Content = page;
        }
    }
}
