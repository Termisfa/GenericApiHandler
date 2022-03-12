using CryptoAlertsBot.ApiHandler.Helpers;
using CryptoAlertsBot.ApiHandler.Models;
using GenericApiHandler;
using GenericApiHandler.Authentication;
using GenericApiHandler.Data.Enums;

namespace CryptoAlertsBot.ApiHandler
{
    public class BuildAndExeApiCall
    {
        private readonly LogEvent _logEvent;
        private readonly AuthToken _authToken;

        public BuildAndExeApiCall(LogEvent logEvent, AuthToken authToken)
        {
            _logEvent = logEvent;
            _authToken = authToken;
        }

        public async Task<List<T>> GetAllTable<T>(string table = default, string schema = default)
        {
            try
            {
                return await GetWithMultipleArguments<T>(default, table, schema);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public Task<List<T>> GetWithOneArgument<T>(string argumentName, string argumentValue, string table = default, string schema = default)
        {
            try
            {
                Dictionary<string, string> parameters = new();
                parameters.Add(argumentName, argumentValue);

                return GetWithMultipleArguments<T>(parameters, table, schema);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<List<T>> GetWithMultipleArguments<T>(Dictionary<string, string> parameters, string table = default, string schema = default)
        {
            try
            {
                table ??= typeof(T).Name.ToLower();

                Response response = await BuildAndExe(ApiCallTypesEnum.Get, table, parameters, schema);

                List<T> result = Parsers.HttpResultToListCustomObject<T>(response.Result);

                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<int> Post(string table, object obj, string schema = default)
        {
            try
            {
                int result = await ExeAndParseIntResult(ApiCallTypesEnum.Post, table, schema: schema, obj: obj);

                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<int> PutWithOneArgument(string table, object obj, string argumentName, string argumentValue, string schema = default)
        {
            try
            {
                Dictionary<string, string> parameters = new();
                parameters.Add(argumentName, argumentValue);

                return await PutWithMultipleArguments(table, obj, parameters, schema);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<int> PutWithMultipleArguments(string table, object obj, Dictionary<string, string> parameters, string schema = default)
        {
            try
            {
                int result = await ExeAndParseIntResult(ApiCallTypesEnum.Put, table, parameters, schema, obj);

                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<int> DeleteWithOneArgument(string table, string argumentName, string argumentValue, string schema = default)
        {
            try
            {
                Dictionary<string, string> parameters = new();
                parameters.Add(argumentName, argumentValue);

                return await DeleteWithMultipleArguments(table, parameters, schema);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<int> DeleteWithMultipleArguments(string table, Dictionary<string, string> parameters, string schema = default)
        {
            try
            {
                int result = await ExeAndParseIntResult(ApiCallTypesEnum.Delete, table, parameters, schema);

                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private async Task<int> ExeAndParseIntResult(ApiCallTypesEnum apiCallType, string table, Dictionary<string, string> parameters = default, string schema = default, object obj = default)
        {
            try
            {
                Response response = await BuildAndExe(apiCallType, table, parameters, schema, obj);

                if (int.TryParse(response.Result, out int result))
                {
                    return result;
                }

                return 0;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private async Task<Response> BuildAndExe(ApiCallTypesEnum apiCallType, string table, Dictionary<string, string> parameters = default, string schema = default, object obj = default)
        {
            try
            {
                schema ??= ApiAppSettingsManager.GetApiDefaultSchema();

                HttpObject httpObject = obj == null ? default : new(obj);

                string uri = ApiUriBuilder.BuildUri(table, parameters);

                var httpResponse = await ApiCalls.ExeCall(apiCallType, uri, httpObject, schema: schema, apiToken: _authToken.Token);

                Response response = HttpResponseHandler.GetResponseFromHttpAsync(httpResponse).Result;

                if (!response.Success)
                {
                    _logEvent.Log(response);
                }

                return response;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
