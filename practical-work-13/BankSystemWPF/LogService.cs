using BankSystemWPF.Model;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace BankSystemWPF
{
    public class LogService
    {
        private LogRepository _repository;
        private IChangeClient _employee;
        public List<ActionLog> actionLogs;

        public LogService(LogRepository repository, IChangeClient employee) 
        { 
            _repository = repository;
            _employee = employee;
            actionLogs = LoadActionLog();
        }

        public List<ActionLog> LoadActionLog()
        {
            return _repository.LoadActionLog();
        }

        public void OnEventTriggered(string args)
        {
            ActionLog newRecord = new ActionLog(args, _employee);
            actionLogs.Add(newRecord);
            _repository.SaveLogRecord(newRecord);
        }


    }
}
