using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BankClients
{
    class Consultant : Employee
    {
        private static string[] fieldsToChange = { "номер телефона" }; //список полей, которые может менять консультант

        public Consultant() : base() { }

        public override bool CanAddClient() 
        {
            return false;
        }

        public override string[] FieldsToChange() 
        { 
            return fieldsToChange; 
        }

        public override bool CanViewClientPassportNumber() 
        {
            return false;
        }
    }
}
