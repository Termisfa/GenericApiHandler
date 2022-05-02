using CryptoAlertsBot.ApiHandler.Helpers;
using GenericApiHandler.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericApiHandler.Models
{
    public class HttpParameter
    {
        public string Column { get; set; }

        public string Value { get; set; }

        public ComparatorsEnum Comparator { get; set; }

        public bool IncludesApostrophes { get; set; }

        public static HttpParameter DefaultParameter(string columnName, string value, ComparatorsEnum comparator = ComparatorsEnum.equals)
        {
            return new HttpParameter()
            {
                Column = columnName,
                Value = value,
                Comparator = comparator,
                IncludesApostrophes = true
            };
        }

        public static HttpParameter DefaultParameter(string columnName, double? value, ComparatorsEnum comparator = ComparatorsEnum.equals)
        {
            return new HttpParameter()
            {
                Column = columnName,
                Value = value.ToString().Replace(',', '.'),
                Comparator = comparator,
                IncludesApostrophes = true
            };
        }

        public static HttpParameter DefaultParameter(string columnName, DateTime dateValue, ComparatorsEnum comparator = ComparatorsEnum.equals)
        {
            string value = Parsers.DatetimeToStringSqlFormat(dateValue);

            return new HttpParameter()
            {
                Column = columnName,
                Value = value,
                Comparator = comparator,
                IncludesApostrophes = true
            };
        }

        public static HttpParameter ParameterWithoutApostrophes(string columnName, string value, ComparatorsEnum comparator = ComparatorsEnum.equals)
        {
            return new HttpParameter()
            {
                Column = columnName,
                Value = value,
                Comparator = comparator,
                IncludesApostrophes = false
            };
        }
    }
}
