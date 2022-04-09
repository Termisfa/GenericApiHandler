using CryptoAlertsBot.ApiHandler.Helpers;
using CryptoAlertsBot.ApiHandler.Models;
using GenericApiHandler;
using GenericApiHandler.Authentication;
using GenericApiHandler.Data.Enums;
using GenericApiHandler.Models;

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
                return await GetWithMultipleParameters<T>(default, table, schema);
            }
            catch (Exception e)
            {
                _logEvent.Log(exc: e);
                return default;
            }
        }

        public Task<List<T>> GetWithOneParameter<T>(HttpParameter parameter, string table = default, string schema = default)
        {
            try
            {
                return GetWithMultipleParameters<T>(new List<HttpParameter>() { parameter }, table, schema);
            }
            catch (Exception e)
            {
                _logEvent.Log(exc: e);
                return default;
            }
        }

        public async Task<List<T>> GetWithMultipleParameters<T>(List<HttpParameter> parameters, string table = default, string schema = default)
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
                _logEvent.Log(exc: e);
                return default;
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
                _logEvent.Log(exc: e);
                return default;
            }
        }

        public async Task<int> PutWithOneParameter(string table, object obj, HttpParameter parameter, string schema = default)
        {
            try
            {
                return await PutWithMultipleParameters(table, obj, new List<HttpParameter>() { parameter }, schema);
            }
            catch (Exception e)
            {
                _logEvent.Log(exc: e);
                return default;
            }
        }

        public async Task<int> PutWithMultipleParameters(string table, object obj, List<HttpParameter> parameters, string schema = default)
        {
            try
            {
                int result = await ExeAndParseIntResult(ApiCallTypesEnum.Put, table, parameters, schema, obj);

                return result;
            }
            catch (Exception e)
            {
                _logEvent.Log(exc: e);
                return default;
            }
        }

        public async Task<int> DeleteWithOneParameter(string table, HttpParameter parameter, string schema = default)
        {
            try
            {
                return await DeleteWithMultipleParameters(table, new List<HttpParameter>() { parameter }, schema);
            }
            catch (Exception e)
            {
                _logEvent.Log(exc: e);
                return default;
            }
        }

        public async Task<int> DeleteWithMultipleParameters(string table, List<HttpParameter> parameters, string schema = default)
        {
            try
            {
                int result = await ExeAndParseIntResult(ApiCallTypesEnum.Delete, table, parameters, schema);

                return result;
            }
            catch (Exception e)
            {
                _logEvent.Log(exc: e);
                return default;
            }
        }

        private async Task<int> ExeAndParseIntResult(ApiCallTypesEnum apiCallType, string table, List<HttpParameter> parameters = default, string schema = default, object obj = default)
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
                _logEvent.Log(exc: e);
                return default;
            }
        }

        private async Task<Response> BuildAndExe(ApiCallTypesEnum apiCallType, string table, List<HttpParameter> parameters = default, string schema = default, object obj = default)
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
                _logEvent.Log(exc: e);
                return default;
            }
        }
    }
}
