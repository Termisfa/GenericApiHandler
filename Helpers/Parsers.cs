using CryptoAlertsBot.Extensions;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;
using System.Text.Json;

namespace CryptoAlertsBot
{
    public static class Parsers
    {
        public static string DatetimeToStringSqlFormat(DateTime date)
        {
            string result = date.ToString("yyyy-MM-dd HH:mm:ss");

            return result;
        }

        public static string SqlFormatedStringToDateTimeFormat(string date)
        {
            if (string.IsNullOrEmpty(date))
                return date;

            string result = DateTime.ParseExact(date, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy HH:mm:ss");

            return result;
        }


        public static List<T> HttpResultToListCustomObject<T>(string rawString) 
        {
            List<T> result = new();

            Type myType = typeof(T);
            IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties().OrderBy(o => ((DisplayAttribute)o.GetCustomAttributes(typeof(DisplayAttribute), false)[0]).Order));

            var resultList = JsonSerializer.Deserialize<List<List<string>>>(rawString);

            foreach (var row in resultList)
            {
                T customObject = (T)Activator.CreateInstance(typeof(T));

                for (int i = 0; i < row.Count; i++)
                {
                    var actualType = Nullable.GetUnderlyingType(props[i].PropertyType) ?? props[i].PropertyType;

                    var preCastedValue = row[i];
                    if (actualType == typeof(DateTime))
                        preCastedValue = Parsers.SqlFormatedStringToDateTimeFormat(preCastedValue);
                    //TODO: add case for double changing . to ,
                    //else if()
                    //    preCastedValue = 

                    var safeValue = (preCastedValue == null || preCastedValue == "null") ? null : Convert.ChangeType(preCastedValue, actualType);

                    customObject.GetType().GetPropertyCustom(i).SetValue(customObject, safeValue);
                }

                result.Add(customObject);
            }

            return result;
        }
    }
}
