using GenericApiHandler.Data.Enums;
using GenericApiHandler.Models;

namespace CryptoAlertsBot.ApiHandler
{
    public static class ApiUriBuilder
    {
        public static string BuildUri(string table = default, List<HttpParameter> parameters = default)
        {
            try
            {
                string result = ApiAppSettingsManager.GetApiBaseUri() + "?table=" + table;

                if (parameters != default)
                {
                    string comparatorSides = "$$$";
                    result += "&parameters=(";

                    foreach (HttpParameter parameter in parameters)
                    {
                        string apostrophe = parameter.IncludesApostrophes ? "'" : "";

                        string comparator = string.Empty;

                        switch (parameter.Comparator)
                        {
                            case ComparatorsEnum.equals:
                                comparator = "=";
                                break;
                            case ComparatorsEnum.lowerThan:
                                comparator = "<";
                                break;
                            case ComparatorsEnum.lowerOrEqualThan:
                                comparator = "<=";
                                break;
                            case ComparatorsEnum.greaterThan:
                                comparator = ">";
                                break;
                            case ComparatorsEnum.greaterOrEqualThan:
                                comparator = ">=";
                                break;
                            default:
                                break;
                        }

                        result += parameter.Column + comparatorSides + comparator + comparatorSides + apostrophe + parameter.Value + apostrophe + "|$|";
                    }

                    result = result.Substring(0, result.Length - 3); //To remove the last '|$|'
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
