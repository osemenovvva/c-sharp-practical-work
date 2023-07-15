using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystemWPF.Model
{
    class Consultant : Employee
    {
        public Consultant() : base() { }

        public override bool CanAddClient()
        {
            return false;
        }

        public override bool CanViewClientPassportNumber()
        {
            return false;
        }

        public override bool CanUpdateLastName()
        {
            return false;
        }

        public override bool CanUpdateFirstName()
        {
            return false;
        }

        public override bool CanUpdateMiddleName()
        {
            return false;
        }

        public override bool CanUpdatePhoneNumber()
        {
            return true;
        }

        public override bool CanUpdatePassportNumber()
        {
            return false;
        }
    }
}
