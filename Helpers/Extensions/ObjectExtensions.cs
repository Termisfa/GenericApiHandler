using CryptoAlertsBot.ApiHandler.Helpers;
using CryptoAlertsBot.Extensions;
using System.Globalization;
using System.Reflection;

namespace GenericApiHandler.Helpers.Extensions
{
    public static class ObjectExtensions
    {
        public static void CastAndAssignValueToObject(this Object? obj, PropertyInfo propertyInfo, dynamic preCastedValue, int propertyOrderIndex)
        {
            try
            {
                dynamic? safeValue = null;

                if (preCastedValue != null && preCastedValue != "null")
                {
                    var actualType = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;

                    if (actualType == typeof(DateTime))
                        preCastedValue = Parsers.SqlFormatedStringToDateTimeFormat(preCastedValue);

                    safeValue = Convert.ChangeType(preCastedValue, actualType, CultureInfo.InvariantCulture);
                }

                obj.GetType().GetPropertyCustom(propertyOrderIndex).SetValue(obj, safeValue);
            }
            catch (Exception e) { throw; }
        }
    }
}
