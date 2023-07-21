using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BankSystemLibrary.DTO
{
    public class AccountDTO : INotifyPropertyChanged
    {
        #region Поля

        int id;
        string accountName;
        string creationDate;
        AccountType type;
        string balance;
        int clientId;

        #endregion

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        #region Свойства

        /// <summary>
        /// Идентификатор cчета
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название cчета
        /// </summary>
        public string AccountName
        {
            get
            {
                return accountName;
            }

            set
            {
                accountName = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Дата открытия счета
        /// </summary>
        public string CreationDate { get; set; }

        /// <summary>
        /// Тип (депозитный или недепозитный)
        /// </summary>
        public AccountType Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Баланс счета
        /// </summary>
        public string Balance
        {
            get
            {
                return balance;
            }

            set
            {
                balance = value;
                OnPropertyChanged();
            }
        }

        public int ClientId { get; set; }

        #endregion

        #region Конструкторы

        public AccountDTO(int id, string accountName, string creationDate, AccountType type, string balance, int clientId)
        {
            Id = id;
            AccountName = accountName;
            CreationDate = creationDate;
            Type = type;
            Balance = balance;
            ClientId = clientId;
        }

        public AccountDTO()
        {
        }

        #endregion
    }

    public enum AccountType
    {
        NoDepositAccount = 0,
        DepositAccount = 1
    }
}

