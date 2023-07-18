using System;

namespace BankSystemWPF.Model
{
    public class Client
    {
        #region Поля

        int id;
        string lastName;
        string firstName;
        string middleName;
        string phoneNumber;
        string passportNumber;
        string updateDate;
        string updatedField;
        string updateType;
        string employeeType;


        #endregion

        #region Свойства

        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Имя
        /// </summary>    
        public string FirstName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Номер телефона
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Номер паспорта
        /// </summary>
        public string PassportNumber { get; set; }

        /// <summary>
        /// Дата изменения
        /// </summary>
        public string UpdateDate { get; set; }

        /// <summary>
        /// Измененное поле
        /// </summary>
        public string UpdatedField { get; set; }

        /// <summary>
        /// Тип изменения
        /// </summary>
        public string UpdateType { get; set; }

        /// <summary>
        /// Тип сотрудника
        /// </summary>
        public string EmployeeType { get; set; }

        #endregion

        #region Конструкторы

        public Client(int id, string lastName, string firstName, string middleName, string phoneNumber,
            string passportNumber)
        {
            Id = id;
            LastName = lastName;
            FirstName = firstName;
            MiddleName = middleName;
            PhoneNumber = phoneNumber;
            PassportNumber = passportNumber;
        }

        public Client() { }

        #endregion

    }
}
