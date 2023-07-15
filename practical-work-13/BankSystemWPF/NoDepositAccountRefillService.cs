using BankSystemWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystemWPF
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
            NoDepositAccount noDepositAccount = _repository.FindAccount(clientId, type);
            noDepositAccount.RefillNoDepositAccount();
            noDepositAccount.Balance += refillAmount;
            _repository.RefillAccount(noDepositAccount);
            NoDepositAccountRefilled?.Invoke($"Недепозитный счет пополнен на {refillAmount} у.е.");
            return noDepositAccount;
        }
    }
}
