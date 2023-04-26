using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankClients
{
    class Manager : Employee
    {
        private static string[] fieldsToChange = { "фамилия", "имя", "отчество",
                            "номер телефона", "номер паспорта" }; //список полей, которые может менять менеджер
        
        public Manager() : base() { }

        public override bool CanAddClient()
        {
            return true;
        }

        public override string[] FieldsToChange() 
        {
            return fieldsToChange;
        }

        public override bool CanViewClientPassportNumber() 
        {
            return true;
        }
    }
}
