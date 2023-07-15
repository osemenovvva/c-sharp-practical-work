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
        //private SqliteDataAccess<NoDepositAccount> _noDepositRepository; // Репозиторий для работы с недепозитными счетами
        //private SqliteDataAccess<DepositAccount> _depositRepository; // Репозиторий для работы с депозитными счетами
        private NoDepositAccountRefillService _noDepositAccountRefillService; // Сервис для работы с недепозитными счетами
        private DepositAccountRefillService _depositAccountRefillService; // Сервис для работы с депозитными счетами

        public event Action<string> AccountOpened;
        public event Action<string> AccountClosed;
        public event Action<string> AccountUpdated;
        public event Action<string> MoneyTransfered;
        public event Action<string> ClientUpdated;

        public Service(SqliteDataAccess<Account> repository, NoDepositAccountRefillService noDepositAccountRefillService,
            DepositAccountRefillService depositAccountRefillService)
        {
            _repository = repository;
            //_noDepositRepository = new SqliteDataAccess<NoDepositAccount>();
            //_depositRepository = new SqliteDataAccess<DepositAccount>();
            _depositAccountRefillService = depositAccountRefillService;
            _noDepositAccountRefillService = noDepositAccountRefillService;
        }

        /// <summary>
        /// Метод получения данных о клиентах
        /// </summary>
        /// <returns>Список клиентов</returns>
        public List<ClientDTO> LoadClients()
        {
            List<ClientDTO> clientsDTO = TransformClientsToClientsDTO();
            return clientsDTO;
        }

        /// <summary>
        /// Метод отображения данных клиента
        /// </summary>
        /// <param name="employee">Сотрудник, работающий в системе</param>
        public List<ClientDTO> GetAllClientsView(IChangeClient employee)
        {
            List<ClientDTO> clientsDTO = TransformClientsToClientsDTO();

            if (!employee.CanViewClientPassportNumber())
            {
                clientsDTO.ForEach(x => x.PassportNumber = "******************");
            }

            return clientsDTO;
        }

        /// <summary>
        /// Метод добавления данных о клиенте в хранилище
        /// </summary>
        /// <param name="client">Запись о клиенте</param>
        /// <param name="employee">Сотрудник, работающий в системе</param>
        public void AddClient(ClientDTO client, IChangeClient employee)
        {
            if (employee.CanAddClient())
            {
                Client newClient = this.TransformClientDtoToClient(client);
                _repository.SaveClient(newClient);
            }
        }

        /// <summary>
        /// Метод изменения записи о выбранном клиенте
        /// </summary>
        /// <param name="clientDTO">Выбранная запись о клиенте</param>
        /// <param name="employee">Сотрудник, работающий в системе</param>
        public void UpdateClient(ClientDTO clientDTO, IChangeClient employee)
        {
            Client clientToUpdate = _repository.GetClientById(clientDTO.Id);
            string employeeType = employee.GetType().Name;
            List<string> updatedFieldsList = new List<string>();

            if (employee.CanUpdateLastName() && (clientToUpdate.LastName != clientDTO.LastName))
            {
                clientToUpdate.LastName = clientDTO.LastName;
                updatedFieldsList.Add("Фамилия");
            }

            if (employee.CanUpdateFirstName() && (clientToUpdate.FirstName != clientDTO.FirstName))
            {
                clientToUpdate.FirstName = clientDTO.FirstName;
                updatedFieldsList.Add("Имя");
            }

            if (employee.CanUpdateMiddleName() && (clientToUpdate.MiddleName != clientDTO.MiddleName))
            {
                clientToUpdate.MiddleName = clientDTO.MiddleName;
                updatedFieldsList.Add("Отчество");
            }

            if (employee.CanUpdatePhoneNumber() && (clientToUpdate.PhoneNumber != clientDTO.PhoneNumber))
            {
                clientToUpdate.PhoneNumber = clientDTO.PhoneNumber;
                updatedFieldsList.Add("Номер телефона");
            }

            if (employee.CanUpdatePassportNumber() && (clientToUpdate.PassportNumber != clientDTO.PassportNumber))
            {
                clientToUpdate.PassportNumber = clientDTO.PassportNumber;
                updatedFieldsList.Add("Номер паспорта");
            }

            string updatedFields = String.Join(", ", updatedFieldsList);

            UpdateSystemFields(updatedFields, clientToUpdate, employeeType);
            _repository.EditClient(clientToUpdate);
            ClientUpdated?.Invoke($"Данные клиента обновлены");

        }

        /// <summary>
        /// Метод заполнения системных полей
        /// </summary>
        /// <param name="updatedFields">Измененные поля</param>
        /// <param name="client">Запись о клиенте</param>
        /// <param name="employeeType">Тип сотрудника, вносившего изменение</param>
        public void UpdateSystemFields(string updatedFields, Client client, string employeeType)
        {
            client.UpdateDate = DateTime.Now.ToString();
            client.UpdatedField = updatedFields;
            client.UpdateType = "Изменение данных";
            if (employeeType == "Consultant")
            {
                client.EmployeeType = "Консультант";
            }
            else if (employeeType == "Manager")
            {
                client.EmployeeType = "Менеджер";
            }
        }

        /// <summary>
        /// Метод преобразования отображаемых данных о клиенте в данные для хранилища
        /// </summary>
        /// <param name="clientDTO">Отображаемые данные о клиенте</param>
        /// <returns>Данные для записи в хранилище</returns>
        public Client TransformClientDtoToClient(ClientDTO clientDTO)
        {
            Client client = new Client()
            {
                Id = clientDTO.Id,
                LastName = clientDTO.LastName,
                FirstName = clientDTO.FirstName,
                MiddleName = clientDTO.MiddleName,
                PhoneNumber = clientDTO.PhoneNumber,
                PassportNumber = clientDTO.PassportNumber,
                UpdateDate = clientDTO.UpdateDate,
                UpdatedField = clientDTO.UpdatedField,
                UpdateType = clientDTO.UpdateType,
                EmployeeType = clientDTO.EmployeeType
            };
            return client;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<ClientDTO> TransformClientsToClientsDTO()
        {
            List<Client> clients = _repository.LoadClients();
            List<ClientDTO> clientsDTO = clients.Select(x => new ClientDTO()
            {
                Id = x.Id,
                LastName = x.LastName,
                FirstName = x.FirstName,
                MiddleName = x.MiddleName,
                PhoneNumber = x.PhoneNumber,
                PassportNumber = x.PassportNumber,
                UpdateDate = x.UpdateDate,
                UpdatedField = x.UpdatedField,
                UpdateType = x.UpdateType,
                EmployeeType = x.EmployeeType
            }).ToList();

            return clientsDTO;
        }

        /// <summary>
        /// Метод для получения данных о счете клиента для отображения в системе
        /// </summary>
        /// <param name="client">Клиент</param>
        /// <returns>Список счетов клиента для отображения в системе</returns>
        public List<AccountDTO> GetAllAccountsView(ClientDTO clientDTO)
        {
            List<Account> accounts = _repository.LoadAccounts(clientDTO.Id);
            List<AccountDTO> accountsDTO = accounts
                  .Select(x => new AccountDTO()
                  {
                      Id = x.Id,
                      AccountName = x.AccountName,
                      CreationDate = x.CreationDate,
                      Type = x.Type == 1 ? "Депозитный" : "Недепозитный",
                      Balance = x.Balance.ToString()
                  }).ToList();

            return accountsDTO;
        }

        /// <summary>
        /// Метод для сохранения данных о новом счете
        /// </summary>
        /// <param name="accountDTO">Представление счета</param>
        /// <param name="clientDTO">Представление клиента</param>
        /// <param name="accountName">Наименование счета</param>
        /// <param name="type">Тип счета</param>
        /// <param name="balance">Баланс счета</param>
        public void SaveAccount(AccountDTO accountDTO, ClientDTO clientDTO, string accountName, string type, string balance)
        {
            accountDTO.AccountName = accountName;
            accountDTO.Type = type;
            accountDTO.Balance = balance;
            accountDTO.CreationDate = DateTime.Now.ToString();

            var account = TransformAccountDtoToAccount(accountDTO);
            _repository.SaveAccount(account, clientDTO.Id);

            AccountOpened?.Invoke($"Для клиента {clientDTO.LastName} {clientDTO.FirstName} {clientDTO.MiddleName} открыт счет \"{accountName}\"");
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
        /// <param name="clientId">Идентификатор клиента</param>
        public void EditAccount(AccountDTO accountDTO, int clientId)
        {
            var account = TransformAccountDtoToAccount(accountDTO);
            _repository.EditAccount(account, clientId);
            AccountUpdated?.Invoke($"Обновлен счет \"{accountDTO.AccountName}\"");
        }

        /// <summary>
        /// Метод удаления данных о счете
        /// </summary>
        /// <param name="accountDTO">Представление счета</param>
        public void DeleteAccount(AccountDTO accountDTO)
        {
            var account = TransformAccountDtoToAccount(accountDTO);
            _repository.DeleteAccount(account);
            AccountClosed?.Invoke($"Закрыт счет \"{accountDTO.AccountName}\"");
        }

        /// <summary>
        /// Метод для перевода денежных средств между счетами
        /// </summary>
        /// <param name="accountFromDTO">Предствление счета, с которого клиент переводит деньги</param>
        /// <param name="accountToDTO">Предствление счета, на который клиент переводит деньги</param>
        /// <param name="transferAmount">Сумма перевода</param>
        /// <returns>Массив с обновленными значениями баланса исходного и целевого счетов</returns>
        public void TransferMoney(AccountDTO accountFromDTO, AccountDTO accountToDTO, decimal transferAmount)
        {
            var accountFrom = TransformAccountDtoToAccount(accountFromDTO);
            var accountTo = TransformAccountDtoToAccount(accountToDTO);

            accountFrom.Balance -= transferAmount;
            accountTo.Balance += transferAmount;

            _repository.UpdateBalance(accountFrom);
            _repository.UpdateBalance(accountTo);

            MoneyTransfered?.Invoke($"Деньги переведены со счета \"{accountFrom.AccountName}\" на счет \"{accountTo.AccountName}\"");
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
        /// <param name="clientId">Идентификатор клиента</param>
        /// <param name="accountType">Тип счета</param>
        /// <param name="refillAmount">Сумма пополнения</param>
        /// <returns>Новое значение баланса для отображения</returns>
        public string RefillAccount(int clientId, string accountType, decimal refillAmount)
        {
            var type = (accountType == "Депозитный") ? 1 : 0;

            if (type == 1)
            {
                var depositAccount = _depositAccountRefillService.RefillAccount(clientId, type, refillAmount);
                return depositAccount.Balance.ToString();
            }
            else
            {
                var nodepositAccount = _noDepositAccountRefillService.RefillAccount(clientId, type, refillAmount);
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
        public List<ClientDTO> GetClientsToTranferMoney(ClientDTO currentClient)
        {
            var clients = TransformClientsToClientsDTO();
            return clients.Where(x => x.Id != currentClient.Id).ToList();
        }

    }
}
