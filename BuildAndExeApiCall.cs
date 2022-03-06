﻿using CryptoAlertsBot.ApiHandler.Helpers;
using CryptoAlertsBot.ApiHandler.Models;
using GenericApiHandler;
using GenericApiHandler.Data.Enums;

namespace CryptoAlertsBot.ApiHandler
{
    public class BuildAndExeApiCall
    {
        private readonly LogEvent _logEvent;

        public BuildAndExeApiCall(LogEvent logEvent)
        {
            _logEvent = logEvent;
        }

        public async Task<List<T>> GetAllTable<T>(string table = default, string schema = default)
        {
            return await GetWithMultipleArguments<T>(default, table, schema);
        }

        public Task<List<T>> GetWithOneArgument<T>(string argumentName, string argumentValue, string table = default, string schema = default)
        {
            Dictionary<string, string> args = new();
            args.Add(argumentName, argumentValue);

            return GetWithMultipleArguments<T>(args, table, schema);
        }

        public async Task<List<T>> GetWithMultipleArguments<T>(Dictionary<string, string> parameters, string table = default, string schema = default)
        {
            table ??= typeof(T).Name.ToLower();

            Response response = await BuildAndExe(ApiCallTypesEnum.Get, table, parameters, schema);

            List<T> result = Parsers.HttpResultToListCustomObject<T>(response.Result);

            return result;
        }

        public async Task<int> Post(string table, object obj, string schema = default)
        {
            int result = await ExeAndParseIntResult(ApiCallTypesEnum.Post, table, schema, obj);

            return result;
        }

        public async Task<int> PutWithOneArgument(string table, object obj, string argumentName, string argumentValue, string schema = default)
        {
            Dictionary<string, string> args = new();
            args.Add(argumentName, argumentValue);

            return await PutWithMultipleArguments(table, obj, args, schema);
        }

        public async Task<int> PutWithMultipleArguments(string table, object obj, Dictionary<string, string> args, string schema = default)
        {
            int result = await ExeAndParseIntResult(ApiCallTypesEnum.Put, table, schema, obj);

            return result;
        }

        public async Task<int> DeleteWithOneArgument(string table, string argumentName, string argumentValue, string schema = default)
        {
            Dictionary<string, string> args = new();
            args.Add(argumentName, argumentValue);

            return await DeleteWithMultipleArguments(table, args, schema);
        }

        public async Task<int> DeleteWithMultipleArguments(string table, Dictionary<string, string> args, string schema = default)
        {
            int result = await ExeAndParseIntResult(ApiCallTypesEnum.Delete, table, schema);

            return result;
        }

        private async Task<int> ExeAndParseIntResult(ApiCallTypesEnum apiCallType, string table, string schema = default, object obj = default)
        {
            Response response = await BuildAndExe(apiCallType, table, schema: schema, obj: obj);

            if (int.TryParse(response.Result, out int result))
            {
                return result;
            }

            return 0;
        }


        private async Task<Response> BuildAndExe(ApiCallTypesEnum apiCallType, string table, Dictionary<string, string> parameters = default, string schema = default, object obj = default)
        {
            schema ??= ApiAppSettingsManager.GetApiDefaultSchema();

            HttpObject httpObject = obj == null ? default : new(schema, table, obj);

            string uri = ApiUriBuilder.BuildUri(apiCallType, schema, table, parameters);

            var httpResponse = await ApiCalls.ExeCall(apiCallType, uri, httpObject);

            Response response = HttpResponseHandler.GetResponseFromHttpAsync(httpResponse).Result;

            if (!response.Success)
            {
                _logEvent.Log(response.ErrorInfo);
            }

            return response;
        }
    }
}
