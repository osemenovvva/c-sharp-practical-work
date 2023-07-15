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
        public MainWindow()
        {
            InitializeComponent();
            NavigateToPage(new AuthPage(this));
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
