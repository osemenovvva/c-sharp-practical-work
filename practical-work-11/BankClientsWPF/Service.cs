using BankClientsWPF.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace BankClientsWPF
{
    public class Service
    {
        const string path = "./clients.csv"; // Путь к файлу с данными
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
        /// <param name="client">Запись о клиенте</param>
        /// <param name="employee">Сотрудник, работающий в системе</param>
        public void AddClient(ClientDTO client, IChangeClient employee)
        {
            if (employee.CanAddClient())
            {
                Client newClient = this.TransformClientDtoToClient(client);
                repository.AddClient(newClient);
            }
        }

        /// <summary>
        /// Метод изменения записи о выбранном клиенте
        /// </summary>
        /// <param name="clientDTO">Выбранная запись о клиенте</param>
        /// <param name="employee">Сотрудник, работающий в системе</param>
        public void UpdateClient(ClientDTO clientDTO, IChangeClient employee)
        {
            Client clientToUpdate = repository.GetClientById(clientDTO.Id);
            string employeeType = employee.GetType().Name;
            List<string> updatedFieldsList = new List<string>();

            if (employee.CanUpdateLastName() && (clientToUpdate.LastName != clientDTO.LastName))
            {
                clientToUpdate.LastName = clientDTO.LastName;
                updatedFieldsList.Add("Фамилия");
            }

            if (employee.CanUpdateFirstName() && (clientToUpdate.FirstName != clientDTO.FirstName))
            {
                clientToUpdate.FirstName = clientDTO.FirstName;
                updatedFieldsList.Add("Имя");
            }
            
            if (employee.CanUpdateMiddleName() && (clientToUpdate.MiddleName != clientDTO.MiddleName))
            {
                clientToUpdate.MiddleName = clientDTO.MiddleName;
                updatedFieldsList.Add("Отчество");
            }

            if (employee.CanUpdatePhoneNumber() && (clientToUpdate.PhoneNumber != clientDTO.PhoneNumber))
            {
                clientToUpdate.PhoneNumber = clientDTO.PhoneNumber;
                updatedFieldsList.Add("Номер телефона");
            }   

            if (employee.CanUpdatePassportNumber() && (clientToUpdate.PassportNumber != clientDTO.PassportNumber))
            {
                clientToUpdate.PassportNumber = clientDTO.PassportNumber;
                updatedFieldsList.Add("Номер паспорта");
            }

            string updatedFields = String.Join(", ", updatedFieldsList);

            UpdateSystemFields(updatedFields, clientToUpdate, employeeType);

            File.WriteAllText(path, string.Empty);

            foreach (var client in repository.GetAllClients())
            {
                repository.SaveToFile(client);
            }

        }

        /// <summary>
        /// Метод заполнения системных полей
        /// </summary>
        /// <param name="updatedFields">Измененные поля</param>
        /// <param name="client">Запись о клиенте</param>
        /// <param name="employeeType">Тип сотрудника, вносившего изменение</param>
        public void UpdateSystemFields(string updatedFields, Client client, string employeeType)
        {
            client.UpdateDate = DateTime.Now.ToString();
            client.UpdatedField = updatedFields;
            client.UpdateType = "Изменение данных";
            if (employeeType == "Consultant")
            {
                client.EmployeeType = "Консультант";
            }
            else if (employeeType == "Manager")
            {
                client.EmployeeType = "Менеджер";
            }
        }

        /// <summary>
        /// Метод преобразования отображаемых данных о клиенте в данные для хранилища
        /// </summary>
        /// <param name="clientDTO">Отображаемые данные о клиенте</param>
        /// <returns>Данные для записи в хранилище</returns>
        public Client TransformClientDtoToClient(ClientDTO clientDTO)
        {
            Client client = new Client()
            {
                Id = clientDTO.Id,
                LastName = clientDTO.LastName,
                FirstName = clientDTO.FirstName,
                MiddleName = clientDTO.MiddleName,
                PhoneNumber = clientDTO.PhoneNumber,
                PassportNumber = clientDTO.PassportNumber,
                UpdateDate = clientDTO.UpdateDate,
                UpdatedField = clientDTO.UpdatedField,
                UpdateType = clientDTO.UpdateType,
                EmployeeType = clientDTO.EmployeeType
            };
            return client;
        }
    }
}
