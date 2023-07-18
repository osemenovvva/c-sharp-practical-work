using BankSystemWPF.Model;
using BankSystemWPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BankSystemWPF.View
{
    /// <summary>
    /// Interaction logic for ClientAccountsPage.xaml
    /// </summary>
    public partial class ClientAccountsPage : Page, INotifyPropertyChanged
    {
        private MainWindow _mainWindow;
        private Service<Client> _service;
        private LogService _logService;
        private NoDepositAccountRefillService _noDepositAccountRefillService;
        private DepositAccountRefillService _depositAccountRefillService;
        private UserNotifications _userNotifications;
        private ClientDTO _currentClient; // Выбранный клиент
        private ObservableCollection<AccountDTO>? _clientAccounts; // Счета выбранного клиента
        private ObservableCollection<AccountDTO>? _accountsToTransfer; // Счета, доступные для перевода денег
        private AccountDTO _selectedAccountToTransfer; // Счет, выбранный для перевода денег
        private ObservableCollection<ClientDTO> _clientsToTransfer; // Клиенты, доступные для перевода денег
        private ClientDTO _selectedClientToTransfer; // Клиент, выбранный для перевода денег
        private AccountDTO _selectedAccount; // Выбранный счет
        private AccountDTO _newAccount = new AccountDTO(); // Новый счет
        private string _selectedAccountType; // Выбранный тип счета
        private List<string> _accountTypesToShow; // Типы счета для отображения данных
        private List<string> _accountTypesToChoose; // Типы счета для выбора


        public ClientAccountsPage(MainWindow mainWindow, ClientDTO currentClient, Service<Client> service, LogService logService, 
            DepositAccountRefillService depositAccountRefillService,NoDepositAccountRefillService noDepositAccountRefillService, UserNotifications userNotifications)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            _service = service;
            _logService = logService;
            _depositAccountRefillService = depositAccountRefillService;
            _noDepositAccountRefillService = noDepositAccountRefillService;
            _userNotifications = userNotifications;
            _currentClient = currentClient;

            _clientAccounts = new ObservableCollection<AccountDTO>(_service.GetAllAccountsView(currentClient));
            _accountTypesToShow = new List<string>() { "Недепозитный", "Депозитный" };
            _accountTypesToChoose = new List<string>() { "Недепозитный", "Депозитный" };

            listBox.ItemsSource = _clientAccounts;
            typeComboBox.ItemsSource = _accountTypesToShow;
            DataContext = this;

            accountNameTextBox.IsEnabled = false;
            typeComboBox.IsEnabled = false;
            balanceTextBox.IsEnabled = false;

            if (!_service.CanAnAccountBeCreated(_clientAccounts))
            {
                openAccountButton.Visibility = Visibility.Hidden;
            }

            SubscribeOnEvents();
        }

        /// <summary>
        /// Метод подписки методов на события
        /// </summary>
        private void SubscribeOnEvents()
        {
            _service.AccountOpened += _logService.OnEventTriggered;
            _service.AccountClosed += _logService.OnEventTriggered;
            _service.AccountUpdated += _logService.OnEventTriggered;
            _service.MoneyTransfered += _logService.OnEventTriggered;
            _depositAccountRefillService.DepositAccountRefilled += _logService.OnEventTriggered;
            _noDepositAccountRefillService.NoDepositAccountRefilled += _logService.OnEventTriggered;
            _service.AccountOpened += _userNotifications.ShowNotificationsAccountOpened;
            _service.AccountClosed += _userNotifications.ShowNotificationAccountClosed;
            _service.AccountUpdated += _userNotifications.ShowNotificationAccountUpdated;
            _service.MoneyTransfered += _userNotifications.ShowNotificationMoneyTransfered;
            _depositAccountRefillService.DepositAccountRefilled += _userNotifications.ShowNotificationDepositAccountRefilled;
            _noDepositAccountRefillService.NoDepositAccountRefilled += _userNotifications.ShowNotificationNoDepositAccountRefilled;
        }

        /// <summary>
        /// Метод для отписывания методов от событий
        /// </summary>
        public void UnsubscribeOnEvents()
        {
            _service.AccountOpened -= _logService.OnEventTriggered;
            _service.AccountClosed -= _logService.OnEventTriggered;
            _service.AccountUpdated -= _logService.OnEventTriggered;
            _service.MoneyTransfered -= _logService.OnEventTriggered;
            _depositAccountRefillService.DepositAccountRefilled -= _logService.OnEventTriggered;
            _noDepositAccountRefillService.NoDepositAccountRefilled -= _logService.OnEventTriggered;
            _service.AccountOpened -= _userNotifications.ShowNotificationsAccountOpened;
            _service.AccountClosed -= _userNotifications.ShowNotificationAccountClosed;
            _service.AccountUpdated -= _userNotifications.ShowNotificationAccountUpdated;
            _service.MoneyTransfered -= _userNotifications.ShowNotificationMoneyTransfered;
            _depositAccountRefillService.DepositAccountRefilled -= _userNotifications.ShowNotificationDepositAccountRefilled;
            _noDepositAccountRefillService.NoDepositAccountRefilled -= _userNotifications.ShowNotificationNoDepositAccountRefilled;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        #region Свойства
        /// <summary>
        /// Выбранный счет
        /// </summary>
        public AccountDTO SelectedAccount
        {
            get
            {
                return _selectedAccount;
            }

            set
            {
                _selectedAccount = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Коллекция клиентов для перевода денежных средств
        /// </summary>
        public ObservableCollection<ClientDTO> ClientsToTransfer
        {
            get
            {
                return _clientsToTransfer;
            }
            set
            {
                _clientsToTransfer = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Выбранный клиент для перевода денежных средств
        /// </summary>
        public ClientDTO SelectedClientToTransfer
        {
            get
            {
                return _selectedClientToTransfer;
            }

            set
            {
                _selectedClientToTransfer = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Коллекция доступных счетов для перевода денежных средств
        /// </summary>
        public ObservableCollection<AccountDTO>? AccountsToTransfer
        {
            get
            {
                return _accountsToTransfer;
            }
            set
            {
                _accountsToTransfer = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Выбранный счет для перевода денежных средств
        /// </summary>
        public AccountDTO SelectedAccountToTransfer
        {
            get
            {
                return _selectedAccountToTransfer;
            }

            set
            {
                _selectedAccountToTransfer = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Выбранный тип счета
        /// </summary>
        public string SelectedAccountType
        {
            get
            {
                return _selectedAccountType;
            }

            set
            {
                _selectedAccountType = value;
                OnPropertyChanged("SelectedAccountType");
            }
        }

        #endregion

        private void CreateAccountButton_Click(object sender, RoutedEventArgs e)
        {
            DataContext = _newAccount;

            listBox.IsEnabled = false;

            accountControlButtons.Visibility = Visibility.Hidden;
            subTitle.Visibility = Visibility.Visible;
            subTitle.Content = "Новый счет";

            accountFieldsForm.IsEnabled = true;
            accountNameTextBox.IsReadOnly = false;
            typeComboBox.IsReadOnly = false;
            balanceTextBox.IsReadOnly = false;
            accountNameTextBox.IsEnabled = true;
            typeComboBox.IsEnabled = true;
            balanceTextBox.IsEnabled = true;

            creationDateLabels.Visibility = Visibility.Hidden;
            saveTransferCancelButtons.Visibility = Visibility.Visible;

            typeComboBox.ItemsSource = GetAccountTypesToCreate();
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            UnsubscribeOnEvents();

            if (BankSystemContext.Employee?.GetType() == typeof(Manager))
            {
                _mainWindow.NavigateToPage(new ManagerMainPage(_mainWindow, _logService,
                _depositAccountRefillService, _noDepositAccountRefillService, _service, _userNotifications));
            }
            else if (BankSystemContext.Employee?.GetType() == typeof(Consultant))
            {
                _mainWindow.NavigateToPage(new ConsultantMainPage(_mainWindow, _logService,
                _depositAccountRefillService, _noDepositAccountRefillService, _service, _userNotifications));
            }
        }

        private void SaveAccountButton_Click(object sender, RoutedEventArgs e)
        {
            var accountName = accountNameTextBox.Text;
            var type = typeComboBox.SelectedItem.ToString();
            var balance = balanceTextBox.Text;
            var clientId = _currentClient.Id;

            #region Проверка заполнения полей формы

            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrEmpty(accountName))
            {
                errors.AppendLine("Введите наименование счета");
            }
            if (string.IsNullOrEmpty(type))
            {
                errors.AppendLine("Введите тип счета");
            }
            if (string.IsNullOrEmpty(balance))
            {
                errors.AppendLine("Введите баланс счета");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            #endregion

            _service.SaveAccount(_newAccount, clientId, accountName, type, balance);
            _clientAccounts = new ObservableCollection<AccountDTO>(_service.GetAllAccountsView(_currentClient));
            CancelButton_Click(sender, e);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DataContext = this;
            listBox.IsEnabled = true;

            accountControlButtons.Visibility = Visibility.Hidden;
            subTitle.Visibility = Visibility.Hidden;
            openAccountButton.Visibility = Visibility.Visible;

            accountNameTextBox.IsEnabled = false;
            typeComboBox.IsEnabled = false;
            balanceTextBox.IsEnabled = false;
            accountNameTextBox.IsReadOnly = true;
            typeComboBox.IsReadOnly = true;
            balanceTextBox.IsReadOnly = true;

            accountFieldsForm.Visibility = Visibility.Visible;
            transferMoneyForm.Visibility = Visibility.Hidden;
            refillBalanceForm.Visibility = Visibility.Hidden;
            refillTypeEmptyLabel.Visibility = Visibility.Hidden;
            refillTypeComboBox.Visibility = Visibility.Visible;

            saveTransferCancelButtons.Visibility = Visibility.Hidden;
            saveChangesButton.Visibility = Visibility.Hidden;
            transferButton.Visibility = Visibility.Hidden;
            refillButton.Visibility = Visibility.Hidden;
            saveButton.Visibility = Visibility.Visible;

            refillAmountTextBox.Text = string.Empty;
            transferAmountTextBox.Text = string.Empty;
            chooseAccountComboBox.SelectedIndex = -1;
            chooseClientComboBox.SelectedIndex = -1;

            if (!_service.CanAnAccountBeCreated(_clientAccounts))
            {
                openAccountButton.Visibility = Visibility.Hidden;
            }
        }

        private void EditAccountButton_Click(object sender, RoutedEventArgs e)
        {
            listBox.IsEnabled = false;

            accountControlButtons.Visibility = Visibility.Hidden;
            subTitle.Visibility = Visibility.Visible;
            subTitle.Content = "Изменение счета";

            accountFieldsForm.IsEnabled = true;
            accountNameTextBox.IsReadOnly = false;
            typeComboBox.IsReadOnly = false;
            balanceTextBox.IsReadOnly = false;
            accountNameTextBox.IsEnabled = true;
            typeComboBox.IsEnabled = false;
            balanceTextBox.IsEnabled = false;

            creationDateLabels.Visibility = Visibility.Hidden;
            saveTransferCancelButtons.Visibility = Visibility.Visible;
            saveChangesButton.Visibility = Visibility.Visible;
            saveButton.Visibility = Visibility.Hidden;

            saveChangesButton.IsEnabled = true;
            cancelButton.IsEnabled = true;
        }

        private void ListBox_MouseLeftButonUp(object sender, MouseEventArgs e)
        {
            if (_selectedAccount != null)
            {
                typeComboBox.ItemsSource = GetAccountTypesToShow();
                _selectedAccountType = _service.GetAccountTypeFromDto(_selectedAccount.Type);
                typeComboBox.Text = _selectedAccountType;

                accountControlButtons.Visibility = Visibility.Visible;
            }
        }

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            #region Проверка заполнения полей формы

            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrEmpty(_selectedAccount.AccountName))
            {
                errors.AppendLine("Введите наименование счета");
            }
            if (_selectedAccount?.Type == null)
            {
                errors.AppendLine("Введите тип счета");
            }
            if (string.IsNullOrEmpty(_selectedAccount?.Balance))
            {
                errors.AppendLine("Введите баланс счета");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            #endregion

            _service.EditAccount(_selectedAccount);
            CancelButton_Click(sender, e);
        }

        private void DeleteAccountButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = WPFCustomMessageBox.CustomMessageBox.ShowYesNo("Вы уверены, что хотите закрыть счет?", "Подтверждение закрытия счета", "Да", "Нет");

            if (messageBoxResult == MessageBoxResult.Yes)
            {
                _service.DeleteAccount(_selectedAccount);
                _clientAccounts?.Remove(_selectedAccount);

                CancelButton_Click(sender, e);

                typeComboBox.SelectedIndex = -1;
            }
        }

        private void MoneyTransferButton_Click(object sender, RoutedEventArgs e)
        {
            _accountsToTransfer = new ObservableCollection<AccountDTO>(_service.GetInternalAccountsToTransfer(_clientAccounts, _selectedAccount));

            CheckAvaliableAccountsCount();

            listBox.IsEnabled = false;
            openAccountButton.Visibility = Visibility.Hidden;
            accountControlButtons.Visibility = Visibility.Hidden;
            subTitle.Visibility = Visibility.Visible;
            subTitle.Content = "Перевод денежных средств";

            accountFieldsForm.Visibility = Visibility.Hidden;
            transferMoneyForm.Visibility = Visibility.Visible;

            saveTransferCancelButtons.Visibility = Visibility.Visible;
            saveButton.Visibility = Visibility.Hidden;
            transferButton.Visibility = Visibility.Visible;
        }

        private void TransferConfirmationButton_Click(object sender, RoutedEventArgs e)
        {
            decimal transferAmount = Convert.ToDecimal(transferAmountTextBox.Text);

            #region Проверка заполнения полей формы
            StringBuilder errors = new StringBuilder();

            if (_selectedAccountToTransfer == null)
            {
                errors.AppendLine("Не выбран счет для перевода");
            }
            if (isTransferToOtherClient.IsChecked == true && _selectedClientToTransfer == null)
            {
                errors.AppendLine("Не выбран клиент");
            }
            if (string.IsNullOrEmpty(transferAmountTextBox.Text))
            {
                errors.AppendLine("Введите сумму для перевода");
            }
            if (transferAmount <= 0)
            {
                errors.AppendLine("Неверное значение суммы для перевода");
            }
            if (!string.IsNullOrEmpty(transferAmountTextBox.Text) &&
                transferAmount > Convert.ToDecimal(_selectedAccount.Balance))
            {
                errors.AppendLine("Недостаточно средств");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            #endregion

            _service.TransferMoney(_selectedAccount, _selectedAccountToTransfer, transferAmount);

            _selectedAccount.Balance = _service.GetAccountBalance(_selectedAccount);
            _selectedAccountToTransfer.Balance = _service.GetAccountBalance(_selectedAccountToTransfer);

            CancelButton_Click(sender, e);
        }

        private void TypeComboBox_DropDownOpened(object sender, EventArgs e)
        {
            typeComboBox.ItemsSource = GetAccountTypesToCreate();
        }

        private void RefillAccountButton_Click(object sender, EventArgs e)
        {
            listBox.IsEnabled = false;
            openAccountButton.Visibility = Visibility.Hidden;
            accountControlButtons.Visibility = Visibility.Hidden;
            subTitle.Visibility = Visibility.Visible;
            subTitle.Content = "Пополнение баланса";

            accountFieldsForm.Visibility = Visibility.Hidden;
            refillBalanceForm.Visibility = Visibility.Visible;

            saveTransferCancelButtons.Visibility = Visibility.Visible;
            saveButton.Visibility = Visibility.Hidden;
            refillButton.Visibility = Visibility.Visible;

            refillTypeComboBox.ItemsSource = GetAccountTypesToRefill();
        }

        private void RefillConfirmationButton_Click(object sender, RoutedEventArgs e)
        {
            var refillAmount = Convert.ToDecimal(refillAmountTextBox.Text);

            #region Проверка заполнения полей формы
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrEmpty(refillAmountTextBox.Text))
            {
                errors.AppendLine("Введите сумму пополнения");
            }
            if (refillAmount <= 0)
            {
                errors.AppendLine("Введено некорректное значение");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            #endregion

            var refilledAccount = _service.GetRefilledAccount(_clientAccounts, _selectedAccountType);
            refilledAccount.Balance = _service.RefillAccount(_currentClient.Id, _selectedAccountType, refillAmount);

            CancelButton_Click(sender, e);
        }

        private void IsTranferToOtherClientCheckBox_Checked(object sender, EventArgs e)
        {
            chooseAccountComboBox.SelectedIndex = -1;
            chooseClientLabel.Visibility = Visibility.Visible;
            chooseClientComboBox.Visibility = Visibility.Visible;

            Grid.SetRow(chooseAccountLabel, 3);
            Grid.SetRow(chooseAccountComboBox, 3);
            Grid.SetRow(chooseAccountEmptyLabel, 3);
            Grid.SetRow(transferAmountLabel, 4);
            Grid.SetRow(transferAmountTextBox, 4);

            _clientsToTransfer = new ObservableCollection<ClientDTO>(_service.GetClientsToTranferMoney(_currentClient));

            if (_clientsToTransfer.Count == 0)
            {
                chooseClientEmptyLabel.Visibility = Visibility.Visible;
                chooseClientComboBox.Visibility = Visibility.Hidden;
            }
            else
            {
                chooseClientComboBox.Visibility = Visibility.Visible;
                chooseClientEmptyLabel.Visibility = Visibility.Hidden;
                chooseClientComboBox.ItemsSource = _clientsToTransfer;
            }

            chooseAccountComboBox.IsEnabled = false;
        }

        private void IsTranferToOtherClientCheckBox_Unchecked(object sender, EventArgs e)
        {
            chooseClientLabel.Visibility = Visibility.Hidden;
            chooseClientComboBox.Visibility = Visibility.Hidden;

            Grid.SetRow(chooseAccountLabel, 2);
            Grid.SetRow(chooseAccountComboBox, 2);
            Grid.SetRow(chooseAccountEmptyLabel, 2);
            Grid.SetRow(transferAmountLabel, 3);
            Grid.SetRow(transferAmountTextBox, 3);

            _accountsToTransfer = new ObservableCollection<AccountDTO>(_service.GetInternalAccountsToTransfer(_clientAccounts, _selectedAccount));
            CheckAvaliableAccountsCount();
        }

        private void ChooseClientComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_selectedClientToTransfer != null)
            {
                _accountsToTransfer = new ObservableCollection<AccountDTO>(_service.GetAllAccountsView(_selectedClientToTransfer));
                CheckAvaliableAccountsCount();
            }
        }

        /// <summary>
        /// Метод для проверки наличия счетов у клиента при переводе денег
        /// </summary>
        private void CheckAvaliableAccountsCount()
        {
            if (_accountsToTransfer.Count == 0)
            {
                chooseAccountEmptyLabel.Visibility = Visibility.Visible;
                chooseAccountComboBox.Visibility = Visibility.Hidden;
            }
            else
            {
                chooseAccountEmptyLabel.Visibility = Visibility.Hidden;
                chooseAccountComboBox.IsEnabled = true;
                chooseAccountComboBox.Visibility = Visibility.Visible;
                chooseAccountComboBox.ItemsSource = _accountsToTransfer;
            }
        }

        /// <summary>
        /// Метод для определния списка типов счетов для отображения при выборе счета
        /// </summary>
        /// <returns>Список типов счетов</returns>
        private List<string> GetAccountTypesToShow()
        {
            if (_accountTypesToShow.Count == 1)
            {
                _accountTypesToShow = new List<string>() { "Недепозитный", "Депозитный" };
            }

            return _accountTypesToShow;
        }

        /// <summary>
        /// Метод для отображения доступных для пополнения типов счетов клиента
        /// </summary>
        /// <returns>Список доступных типов счетов</returns>
        private List<string> GetAccountTypesToRefill()
        {
            var currentType = _service.GetAccountTypeFromDto(_clientAccounts[0].Type);
            if (_clientAccounts.Count == 0)
            {
                _accountTypesToChoose = new List<string>() { "Недепозитный", "Депозитный" };
                refillTypeEmptyLabel.Visibility = Visibility.Visible;
                refillTypeComboBox.Visibility = Visibility.Hidden;
            }
            else if (_clientAccounts.Count == 1)
            {
                var typeToDelete = currentType == "Депозитный" ? "Недепозитный" : "Депозитный";
                _accountTypesToChoose.Remove(typeToDelete);
            }

            return _accountTypesToChoose;
        }

        /// <summary>
        /// Метод для отображения доступных типов счетов при создании нового счета
        /// </summary>
        /// <returns>Список доступных типов счетов</returns>
        private List<string> GetAccountTypesToCreate()
        {
            if (_clientAccounts?.Count > 0)
            {
                var currentType = _service.GetAccountTypeFromDto(_clientAccounts[0].Type);

                if (_clientAccounts.Count == 1 &&  _accountTypesToChoose.Count == 2)
                {
                    var typeToDelete = currentType;
                    _accountTypesToChoose.Remove(typeToDelete);
                }
                else if (_clientAccounts.Count == _accountTypesToChoose.Count && currentType == _accountTypesToChoose[0])
                {
                    _accountTypesToChoose[0] = currentType == "Депозитный" ? "Недепозитный" : "Депозитный";
                }
            }
            else if (_clientAccounts?.Count == 0)
            {
                _accountTypesToChoose = new List<string>() { "Недепозитный", "Депозитный" };
            }

            return _accountTypesToChoose;
        }
    }
}