using BankClients.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BankClients
{
    class Service
    {
        const string path = "clients.csv";
        private Repository repository; // Репозиторий для работы с данными

        public Service(Repository repository) {
            this.repository = repository;
        }

        /// <summary>
        /// Метод отображения данных клиента
        /// </summary>
        /// <param name="employee">Сотрудник, работающий в системе</param>
        public ClientDTO[] GetAllClientsView(IChangeClient employee)
        {
            Client[] clients = repository.GetAllClients();
            ClientDTO[] clientsDTO = Array.ConvertAll(clients, x => new ClientDTO(
             x.Id,
             x.LastName,
             x.FirstName,
             x.MiddleName,
             x.PhoneNumber,
             x.PassportNumber,
             x.UpdateDate,
             x.UpdatedField,
             x.UpdateType,
             x.EmployeeType
            ));

            if (!employee.CanViewClientPassportNumber())
            {
                foreach (var client in clientsDTO)
                {
                    client.PassportNumber = "******************";
                }
            }

            return clientsDTO;
        }

        /// <summary>
        /// Метод добавления данных о клиенте в хранилище
        /// </summary>
        /// <param name="сlient">Клиент</param>
        /// <param name="employee">Сотрудник, работающий в системе</param>
        public void AddClient(Client client, IChangeClient employee)
        {
            if (employee.CanAddClient())
            {
                repository.AddClient(client);
            }
            else
            {
                Console.WriteLine("\nНедостаточно прав для добавления клиента");
            }
        }

        /// <summary>
        /// Метод изменения записи о клиенте по идентификатору
        /// </summary>
        /// <param name="clientId">Идентификатор клиента</param>
        /// <param name="field">Поле для изменения</param>
        /// <param name="newData">Новые данные для записи в поле</param>
        /// <param name="employee">Сотрудник, работающий в системе</param>
        public void UpdateClient(int clientId, string field, string newData, IChangeClient employee)
        {
            Client? clientToUpdate = repository.GetClientById(clientId);
            bool canUpdateField = employee.CanUpdateFields(field);
            string employeeType = employee.GetType().Name;

            if (clientToUpdate is not null && !string.IsNullOrEmpty(newData) && canUpdateField)
            {
                switch (field)
                {
                    case "фамилия":
                        clientToUpdate.LastName = newData;
                        break;
                    case "имя":
                        clientToUpdate.FirstName = newData;
                        break;
                    case "отчество":
                        clientToUpdate.MiddleName = newData;
                        break;
                    case "номер телефона":
                        clientToUpdate.PhoneNumber = newData;
                        break;
                    case "номер паспорта":
                        clientToUpdate.PassportNumber = newData;
                        break;
                }

                UpdateSystemFields(field, clientToUpdate, employeeType);

                File.WriteAllText(path, string.Empty);

                foreach (var client in repository.GetAllClients())
                {
                    repository.SaveToFile(client);
                }

            }

            else Console.WriteLine("\nНевозможно обновить поле");

        }

        /// <summary>
        /// Метод заполнения системных полей в записи о клиенте
        /// </summary>
        /// <param name="field">Измененное поле</param>
        /// <param name="client">Запись о клиенте</param>
        /// <param name="employeeType">Тип сотрудника, вносившего изменение</param>
        public void UpdateSystemFields(string field, Client client, string employeeType)
        {
            client.UpdateDate = DateTime.Now.ToString();
            client.UpdatedField = field;
            client.UpdateType = "Изменение данных";
            if (employeeType == "Consultant")
            {
                client.EmployeeType = "Консультант";
            }
            else
            {
                client.EmployeeType = "Менеджер";
            }
        }

    }
}
