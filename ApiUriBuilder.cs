namespace CryptoAlertsBot.ApiHandler
{
    public static class ApiUriBuilder
    {
        public static string GetAndDeleteBuilder(string table, Dictionary<string, string> parameters = default, string? schema = default)
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

        public static string PostBuilder()
        {
            return ApiAppSettingsManager.GetApiBaseUri();
        }


        public static string PutBuilder(Dictionary<string, string> parameters = default)
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
