
namespace BankSystemLibrary.Model
{
    public interface ITransferMoney<in T>
    {
        /// <summary>
        /// Метод обновления баланса счета при переводе денежных средств
        /// </summary>
        /// <param name="account">Счет для изменения баланса</param>
        public void UpdateBalance(T accountFrom, T accountTo);
    }
}
