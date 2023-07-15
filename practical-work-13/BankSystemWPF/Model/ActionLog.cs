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
        string actionDate;
        string actorRole;
        string actionType;

        public string ActionDate { get; set; }

        public string ActorRole { get; set; }

        public string ActionType { get; set; }

        public ActionLog()
        {

        }

        public ActionLog(string actionRecord, IChangeClient employee)
        {
            ActionDate = DateTime.Now.ToString();
            ActorRole = employee.GetType() == typeof(Manager) ? "Менеджер" : "Консультант";
            ActionType = actionRecord;
        }
    }
}
