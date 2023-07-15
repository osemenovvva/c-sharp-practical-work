using System.Windows;

namespace BankSystemWPF.Model
{
    public class DepositAccount : Account
    {
        public DepositAccount() : base() 
        { 
        }

        /// <summary>
        /// Метод пополнения депозитного счета
        /// </summary>
        public void RefillDepositAccount()
        {
            MessageBox.Show("Депозитный счет пополнен успешно");
        }
    }
}
