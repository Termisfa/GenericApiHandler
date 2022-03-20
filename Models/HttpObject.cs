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
            try
            {
                NameValueDict = Helpers.Parsers.ParseObjectToDict(obj);
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
