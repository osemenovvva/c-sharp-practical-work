using BankSystemWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BankSystemWPF
{
    public class DepositAccountRefillService : IRefill<DepositAccount>
    {
        private SqliteDataAccess<DepositAccount> _repository;

        public DepositAccountRefillService(SqliteDataAccess<DepositAccount> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Метод пополнения депозитного счета
        /// </summary>
        /// <param name="client">Клиент</param>
        /// <param name="type">Выбранный тип счета</param>
        /// <param name="refillAmount">Сумма пополнения</param>
        /// <returns>Депозитный счет</returns>
        public DepositAccount RefillAccount(Client client, int type, decimal refillAmount)
        {
            DepositAccount depositAccount = _repository.FindAccount(client, type);
            depositAccount.RefillDepositAccount();
            depositAccount.Balance += refillAmount;
            _repository.RefillAccount(depositAccount);
            return depositAccount;
        }
    }
}
