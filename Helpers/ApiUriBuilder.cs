using GenericApiHandler.Data.Enums;

namespace CryptoAlertsBot.ApiHandler
{
    public static class ApiUriBuilder
    {
        public static string BuildUri(ApiCallTypesEnum apiCallTypes, string schema, string table = default, Dictionary<string, string> parameters = default)
        {
            try
            {
                switch (apiCallTypes)
                {
                    case ApiCallTypesEnum.Get:
                    case ApiCallTypesEnum.Delete:
                        return GetAndDeleteBuilder(table, parameters, schema);
                    case ApiCallTypesEnum.Post:
                        return ApiAppSettingsManager.GetApiBaseUri();
                    case ApiCallTypesEnum.Put:
                        return PutBuilder(parameters);
                    default:
                        return String.Empty;
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private static string GetAndDeleteBuilder(string table, Dictionary<string, string> parameters = default, string? schema = default)
        {
            string result = ApiAppSettingsManager.GetApiBaseUri() + "?schema=";

            result += schema ?? ApiAppSettingsManager.GetApiDefaultSchema();

            result += "&table=" + table;

            if (parameters != default)
            {
                result += "&parameters=(";

                foreach (KeyValuePair<string, string> kvp in parameters)
                {
                    result += kvp.Key + "=" + kvp.Value + "|";
                }

                result = result.Substring(0, result.Length - 1); //To remove the last '|'
                result += ")";
            }

            return result;
        }


        private static string PutBuilder(Dictionary<string, string> parameters = default)
        {
            string result = ApiAppSettingsManager.GetApiBaseUri();

            if (parameters != default)
            {
                result += "?parameters=(";

                foreach (KeyValuePair<string, string> kvp in parameters)
                {
                    result += kvp.Key + "=" + kvp.Value + "|";
                }

                result = result.Substring(0, result.Length - 1); //To remove the last '|'
                result += ")";
            }

            return result;
        }
    }
}
