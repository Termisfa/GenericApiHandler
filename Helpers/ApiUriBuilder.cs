using GenericApiHandler.Data.Enums;

namespace CryptoAlertsBot.ApiHandler
{
    public static class ApiUriBuilder
    {
        public static string BuildUri(string table = default, Dictionary<string, string> parameters = default)
        {
            try
            {
                string result = ApiAppSettingsManager.GetApiBaseUri() + "?table=" + table;

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
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
