using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystemWPF.Model
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
