using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankClients
{
    /// <summary>
    /// Изменение списка клиентов
    /// </summary>
    internal interface IChangeClient
    {
        /// <summary>
        /// Метод для определения прав для добавления нового клиента
        /// </summary>
        /// <returns>Может ли сотрудник добавлять клиентов</returns>
        bool CanAddClient();

        /// <summary>
        /// Метод для определения прав для изменения полей в записи о клиенте
        /// </summary>
        /// <param name="field">Поле для изменения</param>
        /// <returns>Может ли сотрудник менять поле</returns>
        bool CanUpdateFields(string field);

        /// <summary>
        /// Метод для определения прав на просмотр паспортных данных
        /// </summary>
        /// <returns>Может ли сотрудник может просматривать паспортные данные</returns>
        bool CanViewClientPassportNumber();
    }
}
