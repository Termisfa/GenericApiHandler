using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CryptoAlertsBot.Extensions
{
    public static class PropertyInfoExtensions
    {
        public static PropertyInfo GetPropertyCustom(this Type type, int index)
        {
            return type.GetProperties().FirstOrDefault(p => ((DisplayAttribute)p.GetCustomAttributes(typeof(DisplayAttribute), false)[0]).Order == index);
        }

        public static string GetParsedString(this PropertyInfo propertyInfo, object obj)
        {
            string result = string.Empty;

            var objValue = propertyInfo.GetValue(obj);
            var objType = propertyInfo.PropertyType;

            switch (Type.GetTypeCode(objType))
            {
                case TypeCode.DateTime:
                    DateTime dtAux = Convert.ToDateTime(objValue);
                    result = Parsers.DatetimeToStringSqlFormat(dtAux);
                    break;

                case TypeCode.Boolean:
                    bool boolAux = Convert.ToBoolean(objValue);
                    result = boolAux ? "1" : "0";
                    break;

                default:
                    result = Convert.ToString(objValue);
                    break;

            }

            return result;
        }
    }
}
