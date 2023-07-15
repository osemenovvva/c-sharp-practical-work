using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BankSystemWPF.Model
{
    public class AccountDTO : INotifyPropertyChanged
    {
        #region Поля

        int id;
        string accountName;
        string creationDate;
        string type;
        string balance;

        #endregion

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
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
                return this.accountName;
            }

            set
            {
                this.accountName = value;
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
        public string Type
        {
            get
            {
                return this.type;
            }

            set
            {
                this.type = value;
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
                return this.balance;
            }

            set
            {
                this.balance = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Конструкторы

        public AccountDTO(int id, string accountName, string creationDate, string type, string balance)
        {
            Id = id;
            AccountName = accountName;
            CreationDate = creationDate;
            Type = type;
            Balance = balance;
        }

        public AccountDTO()
        {
        }

        #endregion
    }
}

