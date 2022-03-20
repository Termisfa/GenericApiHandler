using CryptoAlertsBot.ApiHandler;
using CryptoAlertsBot.ApiHandler.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GenericApiHandler.Helpers
{
    public class DataBaseBackup
    {
        private readonly BuildAndExeApiCall _buildAndExeApiCall;

        public DataBaseBackup(BuildAndExeApiCall buildAndExeApiCall)
        {
            _buildAndExeApiCall = buildAndExeApiCall;
        }

        public async Task<string> GetDataBaseBackup(List<Type> tableTypes)
        {
            try
            {
                string backupResult = $"-- {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}\n\n";

                foreach (Type tableType in tableTypes)
                {
                    backupResult += "\n\n\n";
                    backupResult += await TableInsertQuerys(tableType);
                }

                return backupResult;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private async Task<string> TableInsertQuerys(Type tableType)
        {
            try
            {
                string tableName = tableType.Name.ToLower();

                string tableBackup = $"-- {tableName} \n\n";
                tableBackup += $"delete from {tableName};\n\n";

                var task = (Task)typeof(BuildAndExeApiCall)
                                .GetTypeInfo()
                                .GetMethod("GetAllTable")
                                .MakeGenericMethod(tableType)
                                .Invoke(_buildAndExeApiCall, new object[] { null, null });
                await task;

                var tableList = (IEnumerable<object>) task.GetType().GetProperty("Result").GetValue(task);

                foreach (object row in tableList)
                {
                    Dictionary<string, string> rowDict = Parsers.ParseObjectToDict(row);
                    tableBackup += $"insert into {tableName}({string.Join(',', rowDict.Keys)}) values ('{string.Join("','", rowDict.Values)}');\n";
                }

                return tableBackup;
            }
            catch (Exception e)
            {
                throw;
            }

        }
    }
}
