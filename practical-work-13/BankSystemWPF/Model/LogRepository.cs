using Dapper;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Linq;
using System.Configuration;

namespace BankSystemWPF.Model
{
    public class LogRepository
    {
        public LogRepository()
        {
        }

        /// <summary>
        /// Метод загрузки логов из хранилища
        /// </summary>
        /// <returns>Список логов</returns>
        public List<ActionLog> LoadActionLog()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<ActionLog>("select * from ActionLog", new DynamicParameters());
                return output.ToList();
            }
        }

        /// <summary>
        /// Метод сохранения данных о действии в системе в хранилище
        /// </summary>
        /// <param name="logRecord">Новый запись о действии в системе</param>
        public void SaveLogRecord(ActionLog logRecord)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into ActionLog (ActionDate, ActorRole, ActionDescription) " +
                    "values (@ActionDate, @ActorRole, @ActionDescription)", logRecord);
            }
        }

        /// <summary>
        /// Метод получения строки подключения к базе
        /// </summary>
        /// <param name="id">Идентификатор строки подключения в конфигурационном файле</param>
        /// <returns>Строка подключения</returns>
        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

    }
}
