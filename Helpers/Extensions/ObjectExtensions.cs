using CryptoAlertsBot;
using CryptoAlertsBot.Extensions;
using System.Reflection;

namespace GenericApiHandler.Helpers.Extensions
{
    public static class ObjectExtensions
    {
        public static void CastAndAssignValueToObject(this Object? obj, PropertyInfo propertyInfo, dynamic preCastedValue, int propertyOrderIndex)
        {
            var actualType = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;

            if (actualType == typeof(DateTime))
                preCastedValue = Parsers.SqlFormatedStringToDateTimeFormat(preCastedValue);
            else if (actualType == typeof(Double))
                preCastedValue = preCastedValue.Replace('.', ',');

            var safeValue = (preCastedValue == null || preCastedValue == "null") ? null : Convert.ChangeType(preCastedValue, actualType);

            obj.GetType().GetPropertyCustom(propertyOrderIndex).SetValue(obj, safeValue);
        }
    }
}
