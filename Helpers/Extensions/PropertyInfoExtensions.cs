using CryptoAlertsBot.ApiHandler.Models;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CryptoAlertsBot.Extensions
{
    public static class PropertyInfoExtensions
    {
        public static string GetParsedString(this PropertyInfo propertyInfo, object obj)
        {
            try
            {
                string result = string.Empty;

                var objValue = propertyInfo.GetValue(obj);

                if (objValue == null)
                    return null;

                var actualType = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;

                if (actualType == typeof(DateTime))
                    result = Parsers.DatetimeToStringSqlFormat(Convert.ToDateTime(objValue));
                else if (actualType == typeof(bool))
                    result = Convert.ToBoolean(objValue) ? "1" : "0";
                else if (actualType == typeof(double))
                    result = Convert.ToString(objValue).Replace(',', '.');
                else
                    result = Convert.ToString(objValue);

                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
