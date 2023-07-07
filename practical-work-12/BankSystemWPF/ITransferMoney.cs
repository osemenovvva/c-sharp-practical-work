using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystemWPF
{
    public interface ITransferMoney<in T>
    {
        /// <summary>
        /// Метод для обновления баланса счета при переводе денежных средств
        /// </summary>
        /// <param name="account">Счет для изменения баланса</param>
        public void UpdateBalance(T account);
    }
}
