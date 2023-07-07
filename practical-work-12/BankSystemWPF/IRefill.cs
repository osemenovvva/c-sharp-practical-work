using BankSystemWPF.Model;

namespace BankSystemWPF
{
    public interface IRefill<out T>
    {
        /// <summary>
        /// Метод пополнения счета
        /// </summary>
        /// <param name="client">Клиент</param>
        /// <param name="type">Выбранный тип счета</param>
        /// <param name="refillAmount">Сумма пополнения</param>
        /// <returns>Счет</returns>
        public T RefillAccount(Client client, int type, decimal refillAmount);
    }
}
