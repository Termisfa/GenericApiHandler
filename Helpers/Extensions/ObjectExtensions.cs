using CryptoAlertsBot;
using CryptoAlertsBot.ApiHandler.Models;
using CryptoAlertsBot.Extensions;
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
                    else if (actualType == typeof(Double))
                        preCastedValue = preCastedValue.Replace('.', ',');

                    safeValue = Convert.ChangeType(preCastedValue, actualType);
                }

                obj.GetType().GetPropertyCustom(propertyOrderIndex).SetValue(obj, safeValue);
            }
            catch (Exception e) { throw; }
        }
    }
}
