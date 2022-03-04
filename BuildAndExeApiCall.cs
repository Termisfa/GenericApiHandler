using CryptoAlertsBot.ApiHandler.Helpers;

namespace CryptoAlertsBot.ApiHandler
{
    public static class BuildAndExeApiCall
    {

        public static async Task<List<T>> GetAllTable<T>(string table = default, string schema = default)
        {
            table ??= typeof(T).Name.ToLower();

            string uri = ApiUriBuilder.GetAndDeleteBuilder(table, default, schema);

            var httpResponse = await ApiCalls.Get(uri);

            List<T> list = await HttpResponseHandler.ResponseGetToObject<T>(httpResponse);

            return list;
        }

        public static Task<List<T>> GetWithOneArgument<T>(string argumentName, string argumentValue, string table = default, string schema = default)
        {
            Dictionary<string, string> args = new();
            args.Add(argumentName, argumentValue);

            return GetWithMultipleArguments<T>(args, table, schema);
        }

        public static async Task<List<T>> GetWithMultipleArguments<T>(Dictionary<string, string> args, string table = default, string schema = default)
        {
            table ??= typeof(T).Name.ToLower();

            string uri = ApiUriBuilder.GetAndDeleteBuilder(table, args, schema);

            var httpResponse = await ApiCalls.Get(uri);

            List<T> list = await HttpResponseHandler.ResponseGetToObject<T>(httpResponse);

            return list;
        }

        public static async Task<int> Post(string table, object obj, string schema = default)
        {
            schema ??= ApiAppSettingsManager.GetApiDefaultSchema();

            HttpObject httpObject = new(schema, table, obj);

            string uri = ApiUriBuilder.PostBuilder();

            var httpResponse = await ApiCalls.Post(uri, httpObject);

            var affectedRows = await HttpResponseHandler.ResponsePostToObject(httpResponse);

            return affectedRows;
        }

        public static async Task<int> PutWithOneArgument(string table, object obj, string argumentName, string argumentValue, string schema = default)
        {
            Dictionary<string, string> args = new();
            args.Add(argumentName, argumentValue);

            schema ??= ApiAppSettingsManager.GetApiDefaultSchema();

            HttpObject httpObject = new(schema, table, obj);

            string uri = ApiUriBuilder.PutBuilder(args);

            var httpResponse = await ApiCalls.Put(uri, httpObject);

            var affectedRows = await HttpResponseHandler.ResponsePutToObject(httpResponse);

            return affectedRows;
        }

        public static async Task<int> PutWithMultipleArguments(string table, object obj, Dictionary<string, string> args, string schema = default)
        {
            schema ??= ApiAppSettingsManager.GetApiDefaultSchema();

            HttpObject httpObject = new(schema, table, obj);

            string uri = ApiUriBuilder.PutBuilder(args);

            var httpResponse = await ApiCalls.Put(uri, httpObject);

            var affectedRows = await HttpResponseHandler.ResponsePutToObject(httpResponse);

            return affectedRows;
        }

        public static async Task<int> DeleteWithMultipleArguments(string table, Dictionary<string, string> args, string schema = default)
        {
            string uri = ApiUriBuilder.GetAndDeleteBuilder(table, args, schema);

            var httpResponse = await ApiCalls.Delete(uri);

            var affectedRows = await HttpResponseHandler.ResponseDeleteToObject(httpResponse);

            return affectedRows;
        }

        public static async Task<int> DeleteWithOneArgument(string table, string argumentName, string argumentValue, string schema = default)
        {
            Dictionary<string, string> args = new();
            args.Add(argumentName, argumentValue);

            string uri = ApiUriBuilder.GetAndDeleteBuilder(table, args, schema);

            var httpResponse = await ApiCalls.Delete(uri);

            var affectedRows = await HttpResponseHandler.ResponseDeleteToObject(httpResponse);

            return affectedRows;
        }


    }
}
