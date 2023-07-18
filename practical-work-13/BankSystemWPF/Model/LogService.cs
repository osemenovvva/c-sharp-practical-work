using System.Collections.Generic;

namespace BankSystemWPF.Model
{
    public class LogService
    {
        private LogRepository _repository;
        public List<ActionLog> actionLogs;

        public LogService(LogRepository repository)
        {
            _repository = repository;
            actionLogs = LoadActionLog();
        }

        /// <summary>
        /// Метод для получения списка логов о действиях в системе
        /// </summary>
        /// <returns>Список логов</returns>
        public List<ActionLog> LoadActionLog()
        {
            return _repository.LoadActionLog();
        }

        /// <summary>
        /// Метод для сохранения логов при возникновении нового события
        /// </summary>
        /// <param name="args">Описание события</param>
        /// <exception cref="System.Exception">Исключение, если сотрудника нет</exception>
        public void OnEventTriggered(string args)
        {
            if (BankSystemContext.Employee != null)
            {
                ActionLog newRecord = new(args);
                actionLogs.Add(newRecord);
                _repository.SaveLogRecord(newRecord);
            } 
            else
            {
                throw new System.Exception();
            }
        }
    }
}
