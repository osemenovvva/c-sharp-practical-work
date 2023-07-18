using System;

namespace BankSystemWPF.Model
{
    public class Account
    {
        #region Поля

        int id;
        string accountName;
        string creationDate;
        int type;
        decimal balance;
        int clientId;

        #endregion 

        #region Свойства

        /// <summary>
        /// Идентификатор cчета
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название cчета
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// Дата открытия счета
        /// </summary>
        public string CreationDate { get; set; }

        /// <summary>
        /// Тип (депозитный или недепозитный)
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// Баланс счета
        /// </summary>
        public decimal Balance { get; set; }

        public int ClientId { get; set; }

        #endregion

        #region Конструкторы

        public Account(int id, string accountName, string creationDate, int type, decimal balance, int clientId)
        {
            Id = id;
            AccountName = accountName;
            CreationDate = creationDate;
            Type = type;
            Balance = balance;
            ClientId = clientId;
        }

        public Account() 
        {
        }

        #endregion
    }
}
