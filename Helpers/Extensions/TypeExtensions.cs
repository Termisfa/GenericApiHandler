using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CryptoAlertsBot.Extensions
{
    public static class TypeExtensions
    {
        public static PropertyInfo GetPropertyCustom(this Type type, int index)
        {
            try
            {
                var result = type.GetProperties().FirstOrDefault(p => GetOrderCustomAttribute(p) == index);
                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public static List<PropertyInfo> GetPropertiesList(this Type type)
        {
            try
            {
                var result = new List<PropertyInfo>(type.GetProperties().OrderBy(p => GetOrderCustomAttribute(p)));
                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private static int GetOrderCustomAttribute(PropertyInfo propertyInfo)
        {
            try
            {
                var result = ((DisplayAttribute)propertyInfo.GetCustomAttributes(typeof(DisplayAttribute), false)[0]).Order;
                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
