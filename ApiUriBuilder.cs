using CryptoAlertsBot.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAlertsBot.ApiHandler
{
    public static class ApiUriBuilder
    {
        public static string GetAndDeleteBuilder(string table, Dictionary<string, string> parameters = default, string? schema = default)
        {
            string result = ApiConstants.BASE_URI + "?schema=";

            result += schema ?? ApiConstants.DB_SCHEMA;

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
            return ApiConstants.BASE_URI;
        }


        public static string PutBuilder(Dictionary<string, string> parameters = default)
        {
            string result = ApiConstants.BASE_URI;

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
