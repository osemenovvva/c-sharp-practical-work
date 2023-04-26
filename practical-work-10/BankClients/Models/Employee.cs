using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankClients
{
    abstract class Employee : IChangeClient
    {
        public Employee() { }

        /// <summary>
        /// Метод для определения прав для добавления нового клиента
        /// </summary>
        /// <returns>Может ли сотрудник добавлять клиентов</returns>
        public abstract bool CanAddClient();

        /// <summary>
        /// Метод для определения прав для изменения полей в записи о клиенте
        /// </summary>
        /// <param name="field">Поле для изменения</param>
        /// <returns>Может ли сотрудник менять поле</returns>
        public bool CanUpdateFields(string field)
        {
            foreach (var fieldToChange in FieldsToChange())
            {
                if (fieldToChange == field) return true;
            }

            return false;
        }

        /// <summary>
        /// Метод для полуения массива полей, которые может менять сотрудник
        /// </summary>
        /// <returns>Массив полей для изменения</returns>
        public abstract string[] FieldsToChange();

        /// <summary>
        /// Метод для определения прав на просмотр паспортных данных
        /// </summary>
        /// <returns>Может ли сотрудник может просматривать паспортные данные</returns>
        public abstract bool CanViewClientPassportNumber();
     
    }
}
