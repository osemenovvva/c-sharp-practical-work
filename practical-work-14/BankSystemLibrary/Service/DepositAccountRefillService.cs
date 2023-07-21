using BankSystemLibrary.Model;
using BankSystemLibrary.Repository;

namespace BankSystemLibrary.Service
{
    public class DepositAccountRefillService : IRefill<DepositAccount>
    {
        private SqliteDataAccess<DepositAccount> _repository;

        public event Action<string> DepositAccountRefilled;

        public DepositAccountRefillService(SqliteDataAccess<DepositAccount> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Метод пополнения депозитного счета
        /// </summary>
        /// <param name="clientId">Идентификатор клиента</param>
        /// <param name="type">Выбранный тип счета</param>
        /// <param name="refillAmount">Сумма пополнения</param>
        /// <returns>Депозитный счет</returns>
        public DepositAccount RefillAccount(int clientId, int type, decimal refillAmount)
        {
            DepositAccount? depositAccount = _repository.FindAccount(clientId, type);

            if (depositAccount != null)
            {
                depositAccount.Balance += refillAmount;
                _repository.RefillAccount(depositAccount);
                DepositAccountRefilled?.Invoke($"Депозитный счет клиента #{clientId} пополнен на {refillAmount} у.е.");

                return depositAccount;
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
