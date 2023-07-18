using System.Windows;

namespace BankSystemWPF.ViewModel
{
    public class UserNotifications
    {
        public UserNotifications() { }

        /// <summary>
        /// Метод для отображения уведомления в системе при открытии счета
        /// </summary>
        /// <param name="args"></param>
        public void ShowNotificationsAccountOpened(string args)
        {
            MessageBox.Show("Счет открыт успешно");
        }

        /// <summary>
        /// Метод для отображения уведомления в системе при закрытии счета
        /// </summary>
        /// <param name="args"></param>
        public void ShowNotificationAccountClosed(string args)
        {
            MessageBox.Show("Счет закрыт успешно");
        }

        /// <summary>
        /// Метод для отображения уведомления в системе при изменении счета
        /// </summary>
        /// <param name="args"></param>
        public void ShowNotificationAccountUpdated(string args)
        {
            MessageBox.Show("Счет изменен успешно");
        }

        /// <summary>
        /// Метод для отображения уведомления в системе при переводе денежных средств
        /// </summary>
        /// <param name="args"></param>
        public void ShowNotificationMoneyTransfered(string args)
        {
            MessageBox.Show("Перевод выполнен успешно");
        }

        /// <summary>
        /// Метод для отображения уведомления в системе при изменении данных клиента
        /// </summary>
        /// <param name="args"></param>
        public void ShowNotificationClientUpdated(string args)
        {
            MessageBox.Show("Данные клиента обновлены успешно");
        }

        /// <summary>
        /// Метод для отображения уведомления в системе при пополнении депозитного счета
        /// </summary>
        /// <param name="args"></param>
        public void ShowNotificationDepositAccountRefilled(string args)
        {
            MessageBox.Show("Депозитный счет пополнен успешно");
        }

        /// <summary>
        /// Метод для отображения уведомления в системе при пополнении недепозитного счета
        /// </summary>
        /// <param name="args"></param>
        public void ShowNotificationNoDepositAccountRefilled(string args)
        {
            MessageBox.Show("Недепозитный счет пополнен успешно");
        }
    }
}
