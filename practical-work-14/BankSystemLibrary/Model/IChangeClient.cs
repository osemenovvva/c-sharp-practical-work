
namespace BankSystemLibrary.Model
{
    /// <summary>
    /// Изменение списка клиентов
    /// </summary>
    /// 
    public interface IChangeClient
    {
        /// <summary>
        /// Метод для определения прав для добавления нового клиента
        /// </summary>
        /// <returns>Может ли сотрудник добавлять клиентов</returns>
        bool CanAddClient();

        /// <summary>
        /// Метод для определения прав на просмотр паспортных данных
        /// </summary>
        /// <returns>Может ли сотрудник просматривать паспортные данные</returns>
        bool CanViewClientPassportNumber();

        /// <summary>
        /// Метод для определения прав на редактирование поля "Фамилия"
        /// </summary>
        /// <returns>Может ли сотрудник изменять поле "Фамилия"</returns>
        bool CanUpdateLastName();

        /// <summary>
        /// Метод для определения прав на редактирование поля "Имя"
        /// </summary>
        /// <returns>Может ли сотрудник изменять поле "Имя"</returns>
        bool CanUpdateFirstName();

        /// <summary>
        /// Метод для определения прав на редактирование поля "Отчество"
        /// </summary>
        /// <returns>Может ли сотрудник изменять поле "Отчество"</returns>
        bool CanUpdateMiddleName();

        /// <summary>
        /// Метод для определения прав на редактирование поля "Номер телефона"
        /// </summary>
        /// <returns>Может ли сотрудник изменять поле "Номер телефона"</returns>
        bool CanUpdatePhoneNumber();

        /// <summary>
        /// Метод для определения прав на редактирование поля "Номер паспорта"
        /// </summary>
        /// <returns>Может ли сотрудник изменять поле "Номер паспорта"</returns>
        bool CanUpdatePassportNumber();
    }
}
