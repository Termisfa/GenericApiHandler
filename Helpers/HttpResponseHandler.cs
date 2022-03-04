namespace CryptoAlertsBot.ApiHandler.Helpers
{
    public static class HttpResponseHandler
    {
        public async static Task<List<T>> ResponseGetToObject<T>(HttpResponseMessage? httpResponseMessage)
        {
            if(!ResponseWasOk(httpResponseMessage, "GET"))
                return new List<T>();

            string resultContent = await httpResponseMessage.Content.ReadAsStringAsync();

            List<T> result = Parsers.HttpResultToListCustomObject<T>(resultContent);

            return result;
        }

        public async static Task<int> ResponsePostToObject(HttpResponseMessage? httpResponseMessage)
        {
            if(!ResponseWasOk(httpResponseMessage, "POST"))
                return 0;

            string resultContent = await httpResponseMessage.Content.ReadAsStringAsync();

            var result = int.Parse(resultContent);

            return result;
        }

        public async static Task<int> ResponsePutToObject(HttpResponseMessage? httpResponseMessage)
        {
            if(!ResponseWasOk(httpResponseMessage, "PUT"))
                return 0;

            string resultContent = await httpResponseMessage.Content.ReadAsStringAsync();

            var result = int.Parse(resultContent);

            return result;
        }

        public async static Task<int> ResponseDeleteToObject(HttpResponseMessage? httpResponseMessage)
        {
            if(!ResponseWasOk(httpResponseMessage, "DELETE"))
                return 0;

            string resultContent = await httpResponseMessage.Content.ReadAsStringAsync();

            var result = int.Parse(resultContent);

            return result;
        }



        public static bool ResponseWasOk(HttpResponseMessage? httpResponseMessage, string method)
        {
            try
            {
                if(httpResponseMessage == null)
                    throw new ArgumentNullException(nameof(httpResponseMessage));

                if(!httpResponseMessage.IsSuccessStatusCode)
                {
                    //Logger.Log($"Error in '{method}' Api call. URI:" + httpResponseMessage.RequestMessage.RequestUri);
                    //Logger.Log(httpResponseMessage.StatusCode.ToString());
                    //Logger.Log(httpResponseMessage.ReasonPhrase);
                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                //Logger.Log(e.Message);
                //Logger.Log(e.StackTrace);
                return false;
            }
        }

    }
}
