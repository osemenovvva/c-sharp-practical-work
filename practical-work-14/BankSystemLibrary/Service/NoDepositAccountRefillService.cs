using BankSystemLibrary.Repository;
using BankSystemLibrary.Model;

namespace BankSystemLibrary.Service
{
    public class NoDepositAccountRefillService : IRefill<NoDepositAccount>
    {
        private SqliteDataAccess<NoDepositAccount> _repository;

        public event Action<string> NoDepositAccountRefilled;

        public NoDepositAccountRefillService(SqliteDataAccess<NoDepositAccount> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Метод пополнения недепозитного счета
        /// </summary>
        /// <param name="clientId">Идентификатор клиента</param>
        /// <param name="type">Выбранный тип счета</param>
        /// <param name="refillAmount">Сумма пополнения</param>
        /// <returns>Недепозитный счет</returns>
        public NoDepositAccount RefillAccount(int clientId, int type, decimal refillAmount)
        {
            NoDepositAccount? noDepositAccount = _repository.FindAccount(clientId, type);

            if (noDepositAccount != null)
            {
                noDepositAccount.Balance += refillAmount;
                _repository.RefillAccount(noDepositAccount);
                NoDepositAccountRefilled?.Invoke($"Недепозитный счет клиента #{clientId} пополнен на {refillAmount} у.е.");

                return noDepositAccount;
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
