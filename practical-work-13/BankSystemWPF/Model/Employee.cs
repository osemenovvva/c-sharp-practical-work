using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystemWPF.Model
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
        /// Метод для определения прав на просмотр паспортных данных
        /// </summary>
        /// <returns>Может ли сотрудник просматривать паспортные данные</returns>
        public abstract bool CanViewClientPassportNumber();

        /// <summary>
        /// Метод для определения прав на редактирование поля "Фамилия"
        /// </summary>
        /// <returns>Может ли сотрудник изменять поле "Фамилия"</returns>
        public abstract bool CanUpdateLastName();

        /// <summary>
        /// Метод для определения прав на редактирование поля "Имя"
        /// </summary>
        /// <returns>Может ли сотрудник изменять поле "Имя"</returns>
        public abstract bool CanUpdateFirstName();

        /// <summary>
        /// Метод для определения прав на редактирование поля "Отчество"
        /// </summary>
        /// <returns>Может ли сотрудник изменять поле "Отчество"</returns>
        public abstract bool CanUpdateMiddleName();

        /// <summary>
        /// Метод для определения прав на редактирование поля "Номер телефона"
        /// </summary>
        /// <returns>Может ли сотрудник изменять поле "Номер телефона"</returns>
        public abstract bool CanUpdatePhoneNumber();

        /// <summary>
        /// Метод для определения прав на редактирование поля "Номер паспорта"
        /// </summary>
        /// <returns>Может ли сотрудник изменять поле "Номер паспорта"</returns>
        public abstract bool CanUpdatePassportNumber();

    }
}
