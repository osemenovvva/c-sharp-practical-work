using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankClients
{
   class Repository
    {
        private Client[] clients; // Основной массив для хранения данных
        protected string path; // Путь к файлу с данными
        int index; // Текущий элемент для добавления в сlients
        int lastId; // Последний использованный id клиента

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="path">Путь в файлу с данными</param>
        public Repository(string path)
        {
            this.clients = new Client[0];
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
            Array.Resize(ref this.clients, this.clients.Length + 1);
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
                    string[] data = sr.ReadLine().Split(',');
                    if (index >= this.clients.Length)
                    {
                        this.Resize();
                    }

                    clients[index] = new Client(int.Parse(data[0]),
                        data[1],
                        data[2],
                        data[3],
                        data[4],
                        data[5],
                        data[6],
                        data[7],
                        data[8],
                        data[9]);
                    
                    index++;
                }
            }
            if (clients.Length > 0)
            {
                lastId = clients[clients.Length - 1].Id;
            }
        }

        /// <summary>
        /// Метод создания файла для хранения записей о клиентах
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
        /// <returns>Массив с записями о клиентах</returns>
        public Client[] GetAllClients()
        {
            return this.clients;
        }

        /// <summary>
        /// Метод получения записи о клиенте по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор клиента</param>
        /// <returns>Запись о найденном клиенте</returns>
        public Client? GetClientById(int id)
        {
            return this.clients.Where(x => x.Id == id).First();
        }

        /// <summary>
        /// Метод добавления данных о клиенте в хранилище
        /// </summary>
        /// <param name="сlient">Клиент</param>
        public void AddClient(Client client)
        {
            if (index >= this.clients.Length)
            {
                this.Resize();
            }

            this.lastId++;

            client.Id = lastId;
            Console.Write("\nВведите фамилию клиента: ");
            client.LastName = Console.ReadLine();
            Console.Write("Введите имя клиента: ");
            client.FirstName = Console.ReadLine();
            Console.Write("Введите отчество клиента: ");
            client.MiddleName = Console.ReadLine();
            Console.Write("Введите номер телефона клиента: ");
            client.PhoneNumber = Console.ReadLine();
            Console.Write("Введите номер паспорта клиента: ");
            client.PassportNumber = Console.ReadLine();

            this.clients[index] = client;

            this.SaveToFile(client);

            this.index++;
        }

        /// <summary>
        /// Метод сохранения записи о клиенте в файл
        /// </summary>
        /// <param name="client">Клиент</param>
        internal void SaveToFile(Client client)
        {
            string line = $"{client.Id},{client.LastName},{client.FirstName},{client.MiddleName},{client.PhoneNumber}," +
            $"{client.PassportNumber},{client.UpdateDate}, {client.UpdatedField}, {client.UpdateType}, {client.EmployeeType}";

            using (StreamWriter sw = new StreamWriter(path, true, Encoding.Unicode))
            {
                sw.WriteLine(line);
            }
        }

    }
}
