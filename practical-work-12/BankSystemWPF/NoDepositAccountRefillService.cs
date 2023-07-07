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

        public NoDepositAccountRefillService(SqliteDataAccess<NoDepositAccount> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Метод пополнения недепозитного счета
        /// </summary>
        /// <param name="client">Клиент</param>
        /// <param name="type">Выбранный тип счета</param>
        /// <param name="refillAmount">Сумма пополнения</param>
        /// <returns>Недепозитный счет</returns>
        public NoDepositAccount RefillAccount(Client client, int type, decimal refillAmount)
        {
            NoDepositAccount noDepositAccount = _repository.FindAccount(client, type);
            noDepositAccount.RefillNoDepositAccount();
            noDepositAccount.Balance += refillAmount;
            _repository.RefillAccount(noDepositAccount);
            return noDepositAccount;
        }
    }
}
