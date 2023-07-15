using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystemWPF.Model
{
    class Manager : Employee
    {
        public Manager() : base() { }

        public override bool CanAddClient()
        {
            return true;
        }

        public override bool CanViewClientPassportNumber()
        {
            return true;
        }

        public override bool CanUpdateLastName()
        {
            return true;
        }

        public override bool CanUpdateFirstName()
        {
            return true;
        }

        public override bool CanUpdateMiddleName()
        {
            return true;
        }

        public override bool CanUpdatePhoneNumber()
        {
            return true;
        }

        public override bool CanUpdatePassportNumber()
        {
            return true;
        }
    }
}
