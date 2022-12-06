namespace Notebook;

using System;
using System.Linq;
using System.Text;

class Repository
{
    private Worker[] workers; // Основной массив для хранения данных
    private string path; // Путь к файлу с данными
    int index; // Текущий элемент для добавления в workers
    int lastId; // Последний использованный id сотрудника


    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="path">Путь в файлу с данными</param>
    public Repository(string path) 
    {
        this.workers = new Worker[0];
        this.path = path;
        this.index = 0;
        this.lastId = 0;

        this.CreateFileIfNotExist();
        this.Load();
    }

    /// <summary>
    /// Метод увеличения текущего хранилища
    /// </summary>
    private void Resize()
    {
        Array.Resize(ref this.workers, this.workers.Length + 1);
    }

    /// <summary>
    /// Метод загрузки записей из файла в хранилище
    /// </summary>
    private void Load()
    {
        using (StreamReader sr = new StreamReader(this.path))
        {
            while (!sr.EndOfStream)
            {
                string[] data = sr.ReadLine().Split('#');
                if (index >= this.workers.Length)
                {
                    this.Resize();
                }

                workers[index] = new Worker(int.Parse(data[0]),
                    DateTime.Parse(data[1]),
                    data[2],
                    int.Parse(data[3]),
                    int.Parse(data[4]),
                    DateTime.Parse(data[5]),
                    data[6]);
                index++;
            }
        }
        if (workers.Length > 0)
        {
            lastId = workers[workers.Length - 1].Id;
        }
    }

    /// <summary>
    /// Метод создания файла для хранения записей о сотрудниках
    /// </summary>
    public void CreateFileIfNotExist()
    {
        if (!File.Exists(path)) 
        {
            StreamWriter sw = new StreamWriter(path);
        }
    }

    /// <summary>
    /// Метод получения данных из хранилища
    /// </summary>
    /// <returns>Массив с записями о сотрудниках</returns>
    public Worker[] GetAllWorkers()
    {
        return this.workers;
    }

    /// <summary>
    /// Метод получения записи о сотруднике по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор сотрудника</param>
    /// <returns>Запись о найденном сотруднике</returns>
    public Worker? GetWorkerById(int id)
    {
        return this.workers.Where(x => x.Id == id).First();
    }

    /// <summary>
    /// Метод удаления записи о сотруднике по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор сотрудника</param>
    public void DeleteWorker(int id)
    {
        Worker? workerToDelete = GetWorkerById(id);
        if (workerToDelete is not null)
        {
            Worker[] newWorkers = workers.Where(x => x.Id != workerToDelete?.Id).ToArray();
            this.workers = newWorkers;
            File.WriteAllText(path, string.Empty);

            foreach (var worker in newWorkers)
            {
                this.SaveToFile(worker);
            }

            index--;
        }

    }

    /// <summary>
    /// Метод добавления данных о сотруднике в хранилище
    /// </summary>
    /// <param name="worker">Сотрудник</param>
    public void AddWorker(Worker worker)
    {
        if (index >= this.workers.Length)
        {
            this.Resize();
        }
        
        this.lastId++;

        worker.Id = lastId;
        worker.CreationDate = DateTime.Now;
        Console.Write("Введите Ф.И.О. сотрудника: ");
        worker.FullName = Console.ReadLine();
        Console.Write("Введите возраст сотрудника: ");
        worker.Age = int.Parse(Console.ReadLine());
        Console.Write("Введите рост сотрудника: ");
        worker.Height = int.Parse(Console.ReadLine());
        Console.Write("Введите дату рождения сотрудника: ");
        worker.BirthDate = DateTime.Parse(Console.ReadLine());
        Console.Write("Введите место рождения сотрудника: ");
        worker.BirthPlace = Console.ReadLine();
        
        this.workers[index] = worker;

        this.SaveToFile(worker);

        this.index++;
    }

    /// <summary>
    /// Метод сохранения записи о сотруднике в файл
    /// </summary>
    /// <param name="worker">Сотрудник</param>
    private void SaveToFile(Worker worker)
    {
        string line = $"{worker.Id}#{worker.CreationDate}#{worker.FullName}#{worker.Age}#{worker.Height}#" +
        $"{worker.BirthDate.ToString("dd.MM.yyyy")}#{worker.BirthPlace}";

        using (StreamWriter sw = new StreamWriter(path, true, Encoding.Unicode))
        {
            sw.WriteLine(line);
        }
    }

    /// <summary>
    /// Метод получения записей о сотруднике в промежутке дат
    /// </summary>
    /// <param name="dateFrom">Первая дата</param>
    /// <param name="dateTo">Вторая дата</param>
    /// <returns>Отфильтрованные записи о сотрудниках</returns>
    public Worker[] GetWorkersBetweenTwoDates(DateTime dateFrom, DateTime dateTo)
    {
        Worker[] filteredWorkers = workers.Where(x => (x.CreationDate >= dateFrom && x.CreationDate <= dateTo)).ToArray();

        return filteredWorkers;
    }
}
