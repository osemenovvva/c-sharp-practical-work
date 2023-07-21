
namespace BankSystemLibrary.Model
{
    public class NoDepositAccount : Account
    {
        public NoDepositAccount() :base() 
        { 
        }

        /// <summary>
        /// Метод пополнения недепозитного счета
        /// </summary>
        public void RefillNoDepositAccount()
        {
            //Логика пополнения недепозитного счета
        }
    }
}
