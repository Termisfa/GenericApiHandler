using CryptoAlertsBot.Extensions;
using GenericApiHandler.Helpers.Extensions;
using GenericApiHandler.Models;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;
using System.Text.Json;

namespace CryptoAlertsBot.ApiHandler.Helpers
{
    public static class Parsers
    {
        public static string DatetimeToStringSqlFormat(DateTime date)
        {
            try
            {
                string result = date.ToString("yyyy-MM-dd HH:mm:ss");
                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public static string SqlFormatedStringToDateTimeFormat(string date)
        {
            try
            {
                DateTime dt = DateTime.Parse(date, CultureInfo.InvariantCulture);

                string result = dt.ToString("dd/MM/yyyy HH:mm:ss");

                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public static List<T> HttpResultToListCustomObject<T>(string rawString)
        {
            try
            {
                if (string.IsNullOrEmpty(rawString))
                {
                    return new(); //TODO CHANGE TO RETURN NULL
                }

                var resultList = JsonSerializer.Deserialize<List<List<string>>>(rawString);
                Dictionary<Type, List<PropertyInfo>> customObjectsList = new();
                List<T> result = new();
                List<PropertyInfo> mainObjProperties = typeof(T).GetPropertiesList();
                bool isMultiObject = mainObjProperties[0].PropertyType.Namespace != "System";

                if (isMultiObject)
                {
                    foreach (var obj in mainObjProperties)
                    {
                        Type typeList = obj.PropertyType;
                        customObjectsList.Add(typeList, typeList.GetPropertiesList());
                    }
                }
                else
                    customObjectsList.Add(typeof(T), mainObjProperties);

                foreach (var row in resultList)
                {
                    T mainObject = (T)Activator.CreateInstance(typeof(T));

                    int sqlColumnIndex = 0;

                    for (int i = 0; i < customObjectsList.Count; i++)
                    {
                        object? customObj;

                        var typeAndPropertyinfoList = customObjectsList.ElementAt(i);

                        customObj = Activator.CreateInstance(Type.GetType(typeAndPropertyinfoList.Key.AssemblyQualifiedName));

                        for (int propertyIndex = 0; propertyIndex < typeAndPropertyinfoList.Value.Count; propertyIndex++)
                        {
                            if (isMultiObject)
                                customObj.CastAndAssignValueToObject(typeAndPropertyinfoList.Value[propertyIndex], row[sqlColumnIndex], propertyIndex);
                            else
                                mainObject.CastAndAssignValueToObject(typeAndPropertyinfoList.Value[propertyIndex], row[sqlColumnIndex], propertyIndex);

                            sqlColumnIndex++;
                        }

                        if (isMultiObject)
                            mainObject.GetType().GetPropertyCustom(i).SetValue(mainObject, customObj);
                    }
                    result.Add(mainObject);
                }

                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public static Dictionary<string, string> ParseObjectToDict(object obj)
        {
            try
            {
                Dictionary<string, string>? dict = new();
                Type myType = obj.GetType();
                IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties().OrderBy(o => ((DisplayAttribute)o.GetCustomAttributes(typeof(DisplayAttribute), false)[0]).Order));
                for (int i = 0; i < props.Count; i++)
                {
                    var propertyInfo = myType.GetPropertyCustom(i);
                    string castedValue = propertyInfo.GetParsedString(obj);

                    if (string.IsNullOrEmpty(castedValue))
                    {
                        continue;
                    }

                    dict.Add(propertyInfo.Name, castedValue);
                }
                return dict;
            }
            catch (Exception e) { throw; }
        }

        public static BulkInsert ParseObjToBulkInsert(object obj)
        {
            try
            {
                BulkInsert result = new BulkInsert();

                List<object> list = ((IEnumerable)obj).Cast<object>().ToList();

                if (list.Count == 0)
                {
                    return result;
                }

                Type myType = list[0].GetType();
                IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties().OrderBy(o => ((DisplayAttribute)o.GetCustomAttributes(typeof(DisplayAttribute), false)[0]).Order));
                for (int i = 0; i < props.Count; i++)
                {
                    var propertyInfo = myType.GetPropertyCustom(i);
                    string castedValue = propertyInfo.GetParsedString(list[0]);

                    if (!string.IsNullOrEmpty(castedValue))
                    {
                        result.ColumnNames.Add(propertyInfo.Name);
                    }

                }

                foreach (var preParsedRow in list)
                {
                    List<string> parsedRow = new();

                    for (int i = 0; i < props.Count; i++)
                    {
                        var propertyInfo = myType.GetPropertyCustom(i);
                        string castedValue = propertyInfo.GetParsedString(preParsedRow);

                        if (!string.IsNullOrEmpty(castedValue))
                        {
                            parsedRow.Add(castedValue);
                        }

                    }

                    result.Rows.Add(parsedRow);
                }

                return result;
            }
            catch (Exception e) { throw; }
        }
    }
}
