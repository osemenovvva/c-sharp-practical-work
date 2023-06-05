using System.Windows;
using System.Windows.Controls;


namespace BankClientsWPF
{
    /// <summary>
    /// Interaction logic for AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        private MainWindow _mainWindow;

        public AuthPage(MainWindow mainWindow)
        {
            InitializeComponent();
            this._mainWindow = mainWindow;
        }

        private void ManagerButton_Click(object sender, RoutedEventArgs e)
        {
            ManagerMainPage managerMainPage = new ManagerMainPage(_mainWindow);
            _mainWindow.NavigateToPage(managerMainPage);
        }

        private void ConsultantButton_Click(object sender, RoutedEventArgs e)
        {
            ConsultantMainPage consultantMainPage = new ConsultantMainPage(_mainWindow);
            _mainWindow.NavigateToPage(consultantMainPage);
        }
    }
}
