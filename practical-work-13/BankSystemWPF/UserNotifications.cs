using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BankSystemWPF
{
    public class UserNotifications
    {
        //Входные параметры не нужны
        public UserNotifications() { }
        public void AccountOpenedNotification(string args)
        {
            MessageBox.Show("Счет открыт успешно");
        }

        public void AccountClosedNotification(string args)
        {
            MessageBox.Show("Счет закрыт успешно");
        }

        public void AccountUpdatedNotification(string args)
        {
            MessageBox.Show("Счет изменен успешно");
        }

        public void MoneyTransferedNotification(string args)
        {
            MessageBox.Show("Перевод выполнен успешно");
        }

        public void ClientUpdatedNotification(string args)
        {
            MessageBox.Show("Данные клиента обновлены успешно");
        }

        public void DepositAccountRefilledNotification(string args)
        {
            MessageBox.Show("Депозитный счет пополнен успешно");
        }

        public void NoDepositAccountRefilledNotification(string args)
        {
            MessageBox.Show("Недепозитный счет пополнен успешно");
        }
    }
}
