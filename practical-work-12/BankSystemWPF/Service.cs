using BankSystemWPF.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;

namespace BankSystemWPF
{
    public class Service<T>
        where T : class
    {
        private SqliteDataAccess<Account> _repository; // Общий репозиторий для работы с данными
        private SqliteDataAccess<NoDepositAccount> _noDepositRepository; // Репозиторий для работы с недепозитными счетами
        private SqliteDataAccess<DepositAccount> _depositRepository; // Репозиторий для работы с депозитными счетами
        private NoDepositAccountRefillService _noDepositAccountRefillService; // Сервис для работы с недепозитными счетами
        private DepositAccountRefillService _depositAccountRefillService; // Сервис для работы с депозитными счетами

        public Service(SqliteDataAccess<Account> repository)
        {
            _repository = repository;
            _noDepositRepository = new SqliteDataAccess<NoDepositAccount>();
            _depositRepository = new SqliteDataAccess<DepositAccount>();
            _depositAccountRefillService = new DepositAccountRefillService(_depositRepository);
            _noDepositAccountRefillService = new NoDepositAccountRefillService(_noDepositRepository);
        }

        /// <summary>
        /// Метод получения данных о клиентах
        /// </summary>
        /// <returns>Список клиентов</returns>
        public List<Client> LoadClients()
        {
            return _repository.LoadClients();
        }

        /// <summary>
        /// Метод для получения данных о счете клиента для отображения в системе
        /// </summary>
        /// <param name="client">Клиент</param>
        /// <returns>Список счетов клиента для отображения в системе</returns>
        public List<AccountDTO> GetAllAccountsView(Client client)
        {
            List<Account> accounts = _repository.LoadAccounts(client);
            List<AccountDTO> accountsDTO = accounts
                  .Select(x => new AccountDTO() {
                      Id = x.Id,
                      AccountName = x.AccountName,
                      CreationDate = x.CreationDate,
                      Type = x.Type == 1 ? "Депозитный" : "Недепозитный",
                      Balance = x.Balance.ToString()
                  }).ToList();

            return accountsDTO;
        }

        /// <summary>
        /// Метод сохранения данных о новом клиенте
        /// </summary>
        /// <param name="client">Новый клиент</param>
        public void SaveClient(Client client)
        {
            _repository.SaveClient(client);
        }

        /// <summary>
        /// Метод для сохранения данных о новом счете
        /// </summary>
        /// <param name="accountDTO">Представление счета</param>
        /// <param name="client">Клиент</param>
        /// <param name="accountName">Наименование счета</param>
        /// <param name="type">Тип счета</param>
        /// <param name="balance">Баланс счета</param>
        public void SaveAccount(AccountDTO accountDTO, Client client, string accountName, string type, string balance)
        {
            accountDTO.AccountName = accountName;
            accountDTO.Type = type;
            accountDTO.Balance = balance;
            accountDTO.CreationDate = DateTime.Now.ToString();

            var account = TransformAccountDtoToAccount(accountDTO);
            _repository.SaveAccount(account, client);
        }

        /// <summary>
        /// Метод преобразования отображаемых данных о счете в данные для хранилища
        /// </summary>
        /// <param name="accountDTO">Представление счета</param>
        /// <returns>Счет</returns>
        public Account TransformAccountDtoToAccount(AccountDTO accountDTO)
        {
            Account account = new Account()
            {
                Id = accountDTO.Id,
                AccountName = accountDTO.AccountName,
                CreationDate = accountDTO.CreationDate,
                Type = accountDTO.Type == "Депозитный" ? 1 : 0,
                Balance = Convert.ToDecimal(accountDTO.Balance)
            };
            return account;
        }

        /// <summary>
        /// Метод для изменения данных о счете
        /// </summary>
        /// <param name="accountDTO">Представление счета</param>
        /// <param name="client">Клиент</param>
        public void EditAccount(AccountDTO accountDTO, Client client)
        {
            var account = TransformAccountDtoToAccount(accountDTO);
            _repository.EditAccount(account, client);
        }

        /// <summary>
        /// Метод удаления данных о счете
        /// </summary>
        /// <param name="accountDTO">Представление счета</param>
        public void DeleteAccount(AccountDTO accountDTO)
        {
            var account = TransformAccountDtoToAccount(accountDTO);
            _repository.DeleteAccount(account);

        }

        /// <summary>
        /// Метод для перевода денежных средств между счетами
        /// </summary>
        /// <param name="accountFromDTO">Предствление счета, с которого клиент переводит деньги</param>
        /// <param name="accountToDTO">Предствление счета, на который клиент переводит деньги</param>
        /// <param name="transferAmount">Сумма перевода</param>
        /// <returns>Массив с обновленными значениями баланса исходного и целевого счетов</returns>
        public string[] TransferMoney(AccountDTO accountFromDTO, AccountDTO accountToDTO, decimal transferAmount)
        {
            var updatedBalance = new string[2];
            var accountFrom = TransformAccountDtoToAccount(accountFromDTO);
            var accountTo = TransformAccountDtoToAccount(accountToDTO);

            accountFrom.Balance -= transferAmount;
            accountTo.Balance += transferAmount;

            updatedBalance[0] = accountFrom.Balance.ToString();
            updatedBalance[1] = accountTo.Balance.ToString();

            _repository.UpdateBalance(accountFrom);
            _repository.UpdateBalance(accountTo);

            return updatedBalance;
        }

        /// <summary>
        /// Метод для проверки возможности создания нового счета
        /// </summary>
        /// <param name="clientAccounts">Коллекция счетов клиента</param>
        /// <returns>Создание счета доступно</returns>
        public bool CanAnAccountBeCreated(ObservableCollection<AccountDTO> clientAccounts)
        {
            return clientAccounts.Count >= 0 && clientAccounts.Count < 2;
        }

        /// <summary>
        /// Метод для пополнения счета клиента по его типу
        /// </summary>
        /// <param name="client">Клиент</param>
        /// <param name="accountType">Тип счета</param>
        /// <param name="refillAmount">Сумма пополнения</param>
        /// <returns>Новое значение баланса для отображения</returns>
        public string RefillAccount(Client client, string accountType, decimal refillAmount)
        {
            var type = (accountType == "Депозитный") ? 1 : 0;

            if (type == 1)
            {
                var depositAccount = _depositAccountRefillService.RefillAccount(client, type, refillAmount);
                return depositAccount.Balance.ToString();
            }
            else
            {
                var nodepositAccount = _noDepositAccountRefillService.RefillAccount(client, type, refillAmount);
                return nodepositAccount.Balance.ToString();
            }
        }

        /// <summary>
        /// Метод для получения счетов для перевода денег между своими счетами
        /// </summary>
        /// <param name="clientAccounts">Список счетов клиента</param>
        /// <param name="selectedAccount">Выбранный счет</param>
        /// <returns>Список доступных для перевода счетов</returns>
        public List<AccountDTO> GetInternalAccountsToTransfer(ObservableCollection<AccountDTO> clientAccounts, AccountDTO selectedAccount)
        {
            return clientAccounts.Where(x => x.Id != selectedAccount.Id).ToList();
        }

        /// <summary>
        /// Метод для получения пополненного счета по его типу
        /// </summary>
        /// <param name="clientAccounts">Список счетов клиента</param>
        /// <param name="accountType">Тип счета</param>
        /// <returns>Предстваление счета</returns>
        public AccountDTO GetRefilledAccount(ObservableCollection<AccountDTO> clientAccounts, string accountType)
        {
            return clientAccounts.Select(x => x).Where(x => x.Type == accountType).FirstOrDefault();
        }

        /// <summary>
        /// Метод для получения клиентов для перевода денежных средств
        /// </summary>
        /// <param name="currentClient">Клиент</param>
        /// <returns>Список доступных для перевода клиентов</returns>
        public List<Client> GetClientsToTranferMoney(Client currentClient)
        {
            var clients = LoadClients();
            return clients.Where(x => x.Id != currentClient.Id).ToList();
        }

    }
}
