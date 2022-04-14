using CryptoAlertsBot;
using CryptoAlertsBot.ApiHandler;
using CryptoAlertsBot.ApiHandler.Helpers;
using System.Reflection;

namespace GenericApiHandler.Helpers
{
    public class DbBackupExecute
    {
        private readonly BuildAndExeApiCall _buildAndExeApiCall;

        public DbBackupExecute(BuildAndExeApiCall buildAndExeApiCall)
        {
            _buildAndExeApiCall = buildAndExeApiCall;
        }

        public async Task<string> GetDataBaseBackup(List<Type> tableTypes)
        {
            try
            {
                string backupResult = $"-- {DateTime.Now:dd/MM/yyyy HH:mm:ss} UTC TIME\n\n";

                string schema = ApiAppSettingsManager.GetApiDefaultSchema();
                backupResult += $"drop database if exists {schema};\n";
                backupResult += $"create database {schema};\n";
                backupResult += $"use {schema};\n";

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

                tableBackup += $"drop table if exists {tableName};\n\n";

                tableBackup += $"{await _buildAndExeApiCall.GetShowCreateTable(tableName)};\n\n";                

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
