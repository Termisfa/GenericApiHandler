using CryptoAlertsBot.ApiHandler.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericApiHandler.Models
{
    public class BulkInsert
    {
        public List<string> ColumnNames { get; set; }

        public List<List<string>> Rows { get; set; }

        public BulkInsert()
        {
            ColumnNames = new();
            Rows = new();
        }

        public static BulkInsert GetBulkInsertFromObj(object obj)
        {
            try
            {
                return Parsers.ParseObjToBulkInsert(obj);
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
