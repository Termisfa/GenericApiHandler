using CryptoAlertsBot.ApiHandler.Helpers;

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
                NameValueDict = Parsers.ParseObjectToDict(obj);
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
