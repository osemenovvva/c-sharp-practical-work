using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystemWPF.Model
{
    public class ActionLog
    {
        #region Поля

        string actionDate;
        string actorRole;
        string actionDescription;

        #endregion

        #region Свойства

        /// <summary>
        /// Дата действия
        /// </summary>
        public string ActionDate { get; set; }

        /// <summary>
        /// Роль пользователя, совершившего действие
        /// </summary>
        public string ActorRole { get; set; }

        /// <summary>
        /// Действие, совершенное в системе
        /// </summary>
        public string ActionDescription { get; set; }

        #endregion

        #region Конструкторы

        public ActionLog()
        {

        }

        public ActionLog(string actionRecord)
        {
            ActionDate = DateTime.Now.ToString();
            ActorRole = BankSystemContext.EmployeeName;
            ActionDescription = actionRecord;
        }

        #endregion
    }
}
