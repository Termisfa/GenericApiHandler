using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CryptoAlertsBot.Extensions
{
    public static class TypeExtensions
    {
        public static PropertyInfo GetPropertyCustom(this Type type, int index)
        {
            var result = type.GetProperties().FirstOrDefault(p => GetOrderCustomAttribute(p) == index);
            return result;
        }

        public static List<PropertyInfo> GetPropertiesList(this Type type)
        {
            var result = new List<PropertyInfo>(type.GetProperties().OrderBy(p => GetOrderCustomAttribute(p)));
            return result;
        }

        private static int GetOrderCustomAttribute(PropertyInfo propertyInfo)
        {
            var result = ((DisplayAttribute)propertyInfo.GetCustomAttributes(typeof(DisplayAttribute), false)[0]).Order;
            return result;
        }
    }
}
