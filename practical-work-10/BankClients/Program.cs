using System.ComponentModel;

namespace BankClients
{
    class Program
    {
        const string path = "clients.csv";
        static void Main(string[] args)
        {
            Repository repository = new Repository(path); // Репозиторий для работы с данными
            Employee employee;

            Console.WriteLine("База данных клиентов\n");
            Console.WriteLine("Выберите роль пользователя системы:\n" +
                    "1 - Консультант\n" +
                    "2 - Менеджер\n");

            int userRole = int.Parse(Console.ReadLine());

            if (userRole == 1)
            {
                employee = new Consultant();
            }
            else if (userRole == 2)
            {
                employee = new Manager();
            }
            else 
            {
                Console.WriteLine("\nВведено неверное значение");
                throw new Exception();
            }

            Service service = new Service(repository);

            while (true)
            {
                Console.Write("\nВыберите действие:\n" +
                    "1 — Вывести список клиентов на экран\n" +
                    "2 - Заполнить данные о клиенте\n" +
                    "3 - Изменить данные клиента\n" +
                    "4 - Выход\n\n");
                int userChoice = int.Parse(Console.ReadLine());
                switch (userChoice)
                {
                    case 1:
                        var clients = service.GetAllClientsView(employee);
                        Array.ForEach(clients, (client) => Console.WriteLine(client));
                        break;
                    case 2:
                        service.AddClient(new Client(), employee);
                        break;
                    case 3:
                        Console.WriteLine("\nВведите идентификатор клиента: ");
                        var selectedClientId = int.Parse(Console.ReadLine());
                        Console.WriteLine("\nВведите название поля для изменения (Фамилия, Имя, Отчество, Номер телефона, Номер паспорта): ");
                        var selectedField = Console.ReadLine().ToLower();
                        Console.WriteLine("\nВведите новое значение поля: ");
                        var newData = Console.ReadLine();
                        Console.WriteLine();
                        service.UpdateClient(selectedClientId, selectedField, newData, employee);
                        break;
                    case 4:
                        return;
                    default:
                        break;
                }

            }
        }
    }
}
