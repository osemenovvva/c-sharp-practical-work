using System.Configuration;
using System.Data;
using System.Data.SQLite;
using Dapper;
using BankSystemLibrary.Model;

namespace BankSystemLibrary.Repository
{
    public class SqliteDataAccess<T> : ITransferMoney<T>
        where T : Account
    {
        /// <summary>
        /// Метод загрузки данных о клиентах из хранилища
        /// </summary>
        /// <returns>Список клиентов</returns>
        public List<Client>? LoadClients()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Client>("select * from Client", new DynamicParameters());
                return output.ToList();
            }
        }

        /// <summary>
        /// Метод получения записи о клиенте по идентификатору
        /// </summary>
        /// <param name="clientId">Идентификатор клиента</param>
        /// <returns>Запись о найденном клиенте</returns>
        public Client? GetClientById(int clientId)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Client>($"select * from Client c where c.Id = {clientId}", new DynamicParameters());
                return output.FirstOrDefault();
            }
        }

        /// <summary>
        /// Метод сохранения данных о новом клиенте в хранилище
        /// </summary>
        /// <param name="client">Новый клиент</param>
        public void SaveClient(Client client)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into Client (LastName, FirstName, MiddleName, PhoneNumber, PassportNumber, UpdateDate, " +
                    "UpdatedField, UpdateType, EmployeeType) " +
                    "values (@LastName, @FirstName, @MiddleName, @PhoneNumber, @PassportNumber, @UpdateDate, @UpdatedField, " +
                    "@UpdateType, @EmployeeType)", client);
            }
        }

        /// <summary>
        /// Метод получения строки подключения к базе
        /// </summary>
        /// <param name="id">Идентификатор строки подключения в конфигурационном файле</param>
        /// <returns>Строка подключения</returns>
        private static string LoadConnectionString(string id = "Default")
        {
            string connectionString = ConfigurationManager.ConnectionStrings[id].ConnectionString;
            return connectionString;
        }

        /// <summary>
        /// Метод изменения данных о клиенте в хранилище
        /// </summary>
        /// <param name="client">Клиент</param>
        public void EditClient(Client client)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute(@"update Client set LastName = @LastName, FirstName = @FirstName, MiddleName = @MiddleName,
                     PhoneNumber = @PhoneNumber, PassportNumber = @PassportNumber, UpdateDate = @UpdateDate,
                     UpdatedField = @UpdatedField, UpdateType = @UpdateType, EmployeeType = @EmployeeType
                     where Id = @Id", client);
            }
        }

        /// <summary>
        /// Метод загрузки данных о счетах клиента из хранилища
        /// </summary>
        /// <param name="clientId">Идентификатор клиента</param>
        /// <returns>Список счетов клиента</returns>
        public List<Account>? LoadAccounts(int clientId)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Account>($"select * from Account a where a.ClientId = {clientId}", new DynamicParameters());
                return output.ToList();
            }
        }

        /// <summary>
        /// Метод сохранения данных о новом счете в хранилище
        /// </summary>
        /// <param name="account">Новый счет</param>
        public void SaveAccount(Account account)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into Account (AccountName, CreationDate, Type, Balance, ClientId) " +
                    $"values (@AccountName, @CreationDate, @Type, @Balance, @ClientId)", account);
            }
        }

        /// <summary>
        /// Метод изменения данных о счете клиента в хранилище
        /// </summary>
        /// <param name="account">Счет</param>
        public void EditAccount(Account account)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute(@"update Account set AccountName = @AccountName, Balance = @Balance where Id = @Id", account);
            }
        }

        /// <summary>
        /// Метод удаления данных о счете из хранилища
        /// </summary>
        /// <param name="account">Счет для удаления</param>
        public void DeleteAccount(Account account)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute($"delete from Account where Id = @Id", account);
            }
        }

        /// <summary>
        /// Метод обновления баланса счета при переводе денежных средств
        /// </summary>
        /// <param name="accountFrom">Счет c которого переведены деньги</param>
        /// <param name="accountTo">Счет на который переведены деньги</param>
        public void UpdateBalance(T accountFrom, T accountTo)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute($"update Account set Balance = @Balance where Id = @Id", accountFrom);
                cnn.Execute($"update Account set Balance = @Balance where Id = @Id", accountTo);
            }
        }

        /// <summary>
        /// Метод пополнения баланса счета
        /// </summary>
        /// <param name="account">Счет</param>
        /// <returns>Счет</returns>
        public Account? RefillAccount(Account account)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute($"update Account set Balance = @Balance where Id = @Id", account);
            }

            return null;
        }

        /// <summary>
        /// Метод поиска счета клиента по его типу в хранилище
        /// </summary>
        /// <param name="clientId">Идентификатор клиента</param>
        /// <param name="accountType">Тип счета</param>
        /// <returns>Счет</returns>
        public T? FindAccount(int clientId, int accountType)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<T>($"select * from Account a where a.ClientId = {clientId} and a.Type = {accountType}",
                                            new DynamicParameters());
                return output.FirstOrDefault();
            }
        }

        /// <summary>
        /// Метод получения счета по идентификатору
        /// </summary>
        /// <param name="accountId">Идентификатор cчета</param>
        /// <returns>Запись о найденном счете</returns>
        public Account? GetAccountById(int accountId)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Account>($"select * from Account a where a.Id = {accountId}", new DynamicParameters());
                return output.FirstOrDefault();
            }
        }
    }
}
