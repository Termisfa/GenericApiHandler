using CryptoAlertsBot.Extensions;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CryptoAlertsBot.ApiHandler.Models
{
    public class HttpObject
    {
        public Dictionary<string, string> NameValueDict { get; set; }

        public HttpObject()
        {
        }

        public HttpObject(object obj)
        {
            NameValueDict = ParseObjectToDict(obj);
        }

        private static Dictionary<string, string> ParseObjectToDict(object obj)
        {
            Dictionary<string, string>? dict = new();

            Type myType = obj.GetType();
            IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties().OrderBy(o => ((DisplayAttribute)o.GetCustomAttributes(typeof(DisplayAttribute), false)[0]).Order));

            for (int i = 0; i < props.Count; i++)
            {
                var propertyInfo = obj.GetType().GetPropertyCustom(i);

                string castedValue = propertyInfo.GetParsedString(obj);

                if (string.IsNullOrEmpty(castedValue))
                    continue;

                var propertyName = propertyInfo.Name;

                dict.Add(propertyName, castedValue);
            }

            return dict;
        }
    }
}
