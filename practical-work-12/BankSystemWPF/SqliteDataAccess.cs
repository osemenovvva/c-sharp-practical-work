using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using Dapper;
using BankSystemWPF.Model;

namespace BankSystemWPF
{
    public class SqliteDataAccess<T> : ITransferMoney<T>
        where T : Account
    {
        /// <summary>
        /// Метод загрузки данных о клиентах из хранилища
        /// </summary>
        /// <returns>Список клиентов</returns>
        public List<Client> LoadClients() 
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Client>("select * from Client", new DynamicParameters());
                return output.ToList();
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
                cnn.Execute("insert into Client (LastName, FirstName, MiddleName, PhoneNumber, PassportNumber) " +
                    "values (@LastName, @FirstName, @MiddleName, @PhoneNumber, @PassportNumber)", client);
            }
        }

        /// <summary>
        /// Метод получения строки подключения к базе
        /// </summary>
        /// <param name="id">Идентификатор строки подключения в конфигурационном файле</param>
        /// <returns>Строка подключения</returns>
        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

        /// <summary>
        /// Метод загрузки данных о счетах клиента из хранилища
        /// </summary>
        /// <param name="client">Клиент</param>
        /// <returns>Список счетов клиента</returns>
        public List<Account> LoadAccounts(Client client)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Account>($"select * from Account a where a.ClientId = {client.Id}", new DynamicParameters());
                return output.ToList();
            }
        }

        /// <summary>
        /// Метод для сохранения данных о новом счете в хранилище
        /// </summary>
        /// <param name="account">Новый счет</param>
        /// <param name="client">Клиент</param>
        public void SaveAccount(Account account, Client client)
        {
            var clientId = client.Id;
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into Account (AccountName, CreationDate, Type, Balance, ClientId) " +
                    $"values (@AccountName, @CreationDate, @Type, @Balance, {clientId})", account);
            }
        }

        /// <summary>
        /// Метод для изменения данных о счете клиента в хранилище
        /// </summary>
        /// <param name="account">Счет</param>
        /// <param name="client">Клиент</param>
        public void EditAccount(Account account, Client client) 
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute($"update Account set AccountName = '{account.AccountName}', CreationDate = '{account.CreationDate}', " +
                    $"Type = '{account.Type}', Balance = '{account.Balance}', ClientId = '{client.Id}' " +
                    $"where Id = {account.Id}");
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
                cnn.Execute($"delete from Account where Id = {account.Id}");
            }
        }

        /// <summary>
        /// Метод для обновления баланса счета при переводе денежных средств
        /// </summary>
        /// <param name="account">Счет для изменения баланса</param>
        public void UpdateBalance(T account)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute($"update Account set Balance = '{account.Balance}' where Id = {account.Id}");
            }
        }

        /// <summary>
        /// Метод для пополнения баланса счета
        /// </summary>
        /// <param name="account">Счет</param>
        /// <returns>Счет</returns>
        public Account RefillAccount(Account account) 
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute($"update Account set Balance = '{account.Balance}' where Id = {account.Id}");
            }

            return null;
        }

        /// <summary>
        /// Метод для поиска счета клиента по его типу в хранилище
        /// </summary>
        /// <param name="client">Клиент</param>
        /// <param name="accountType">Тип счета</param>
        /// <returns>Счет</returns>
        public T FindAccount(Client client, int accountType)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<T>($"select * from Account a where a.ClientId = {client.Id} and a.Type = {accountType}",
                                            new DynamicParameters());
                return output.FirstOrDefault();
            }
        }
    }
}
