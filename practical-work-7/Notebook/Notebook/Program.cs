namespace Notebook
{
    class Program
    {
        static void Main(string[] args)
        {
            const string path = "employees.txt";
            Repository repository = new Repository(path);

            Console.WriteLine("Список сотрудников");
            while (true)
            {
                Console.Write("\nВыберите действие:\n" +
                    "1 — Вывести данные на экран\n" +
                    "2 — Заполнить данные сотрудника\n" +
                    "3 - Вывести данные сотрудника по идентификатору\n" + 
                    "4 - Удалить сотрудника\n" + 
                    "5 - Вывести данные в диапазоне дат\n" +
                    "6 - Сортировать список работников по Ф.И.О.\n" +
                    "7 - Выход\n");
                int userChoice = int.Parse(Console.ReadLine());
                switch (userChoice)
                {
                    case 1:
                        var workers = repository.GetAllWorkers();
                        Array.ForEach(workers, (worker) => Console.WriteLine(worker));
                        break;
                    case 2:
                        repository.AddWorker(new Worker());
                        break;
                    case 3:
                        Console.WriteLine("\nВведите идентификатор сотрудника: ");
                        var selectedWorker = repository.GetWorkerById(int.Parse(Console.ReadLine()));
                        Console.WriteLine(selectedWorker);
                        break;
                    case 4:
                        Console.WriteLine("\nВведите идентификатор сотрудника: ");
                        repository.DeleteWorker(int.Parse(Console.ReadLine()));
                        break;
                    case 5:
                        Console.WriteLine("\nВведите диапазон дат: ");
                        var dateFrom = DateTime.Parse(Console.ReadLine());
                        var dateTo = DateTime.Parse(Console.ReadLine());
                        var filteredWorkers = repository.GetWorkersBetweenTwoDates(dateFrom, dateTo);
                        Array.ForEach(filteredWorkers, (worker) => Console.WriteLine(worker));
                        break;
                    case 6:
                        var sortedWorkers = repository.GetAllWorkers().OrderBy(x => x.FullName).ToArray();
                        Array.ForEach(sortedWorkers, (worker) => Console.WriteLine(worker));
                        break;
                    case 7:
                        return;
                    default:
                        break;
                }
            }
        }
    }
}