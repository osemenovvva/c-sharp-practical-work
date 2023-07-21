
namespace BankSystemLibrary.Model
{
    public class BankSystemContext
    {
        private static IChangeClient? employee;
        private static string? employeeName;

        /// <summary>
        /// Сотрудник, работающий в системе
        /// </summary>
        public static IChangeClient? Employee
        {
            get
            {
                return employee;
            }
            set
            {
                employee = value;
            }
        }

        /// <summary>
        /// Название сотрудника для отображения
        /// </summary>
        public static string? EmployeeName
        {
            get
            {
                return employeeName;
            }
            set
            {
                employeeName = value;
            }
        }


    }
}
