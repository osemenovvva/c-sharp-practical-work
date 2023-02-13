using System.Text;

namespace Files
{
    class Program
    {
        static void Main(string[] args)
        {
            char key = 'y';
            string path = "employees.txt";

            Console.WriteLine("Список сотрудников");
            do
            {
                Console.Write("\nВыберите действие:\n1 — Вывести данные на экран\n2 — Заполнить данные сотрудника\n");
                int userChoice = int.Parse(Console.ReadLine());
                switch (userChoice)
                {
                    case 1:
                        if (File.Exists(path))
                        {
                            PrintFile(path);
                            Console.WriteLine("\nХотите вернуться в меню? (y/n)");
                            key = Console.ReadKey(true).KeyChar;
                        }
                        else
                        {
                            Console.WriteLine("\nДанные о сотрудниках не заполнены, хотите вернуться в меню? (y/n)");
                            key = Console.ReadKey(true).KeyChar;
                        }
                        break;
                    case 2:
                        CreateEditFile(path);
                        Console.WriteLine("\nСотрудник добавлен, хотите продолжить? (y/n)");
                        key = Console.ReadKey(true).KeyChar;
                        break;
                    default:
                        Console.WriteLine("\nВведено неверное значение, хотите продолжить? (y/n)");
                        key = Console.ReadKey(true).KeyChar;
                        break;
                }
            } while (char.ToLower(key) == 'y');
        }

        /// <summary>
        /// Метод для записи данных в файл
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        static void CreateEditFile(string path)
        {
            using (StreamWriter sw = new StreamWriter(path, true, Encoding.Unicode))
            {
                Console.Write("\nВведите идентификатор сотрудника: ");
                int id = int.Parse(Console.ReadLine());
                DateTime recordDate = DateTime.Now;
                Console.Write("Введите Ф.И.О. сотрудника: ");
                string name = Console.ReadLine();
                Console.Write("Введите возраст сотрудника: ");
                int age = int.Parse(Console.ReadLine());
                Console.Write("Введите рост сотрудника: ");
                int height = int.Parse(Console.ReadLine());
                Console.Write("Введите дату рождения сотрудника: ");
                DateTime birthDate = DateTime.Parse(Console.ReadLine());
                Console.Write("Введите место рождения сотрудника: ");
                string birthPlace = Console.ReadLine();


                string employee = $"{id}#{recordDate}#{name}#{age}#{height}#{birthDate.ToString("dd.MM.yyyy")}#{birthPlace}";
                sw.WriteLine(employee);
            }
        }

        /// <summary>
        /// Метод для чтения данных из файла
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        static void PrintFile(string path)
        {
            using (StreamReader sr = new StreamReader(path, Encoding.Unicode))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    string[] data = line.Split('#');
                    Console.WriteLine($"\nИдентификатор: {data[0]}\n" +
                        $"Дата добавления записи: {data[1]}\n" +
                        $"Ф.И.О.: {data[2]}\n" +
                        $"Возраст: {data[3]}\n" +
                        $"Рост: {data[4]}\n" +
                        $"Дата рождения: {data[5]}\n" +
                        $"Место рождения: {data[6]}");
                }
            }
        }

    }
}