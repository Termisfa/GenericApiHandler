using CryptoAlertsBot.ApiHandler.Models;
using Newtonsoft.Json;

namespace CryptoAlertsBot.ApiHandler.Helpers
{
    public static class HttpResponseHandler
    {
        public static async Task<Response> GetResponseFromHttpAsync(HttpResponseMessage? httpResponseMessage)
        {
            try
            {
                Response response;

                if (!ResponseWasOk(httpResponseMessage))
                {
                    response = new()
                    {
                        Result = string.Empty,
                        Success = false,
                        ErrorInfo = new()
                        {
                            Message = $"Status code: {httpResponseMessage.StatusCode}. Reason: {httpResponseMessage.ReasonPhrase}",
                            StackTrace = String.Empty
                        }
                    };
                }
                else
                {
                    string responseString = await httpResponseMessage.Content.ReadAsStringAsync();
                    response = JsonConvert.DeserializeObject<Response>(responseString);
                }

                return response;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async static Task<List<T>> ResponseGetToObject<T>(string resultContent)
        {
            List<T> result = Parsers.HttpResultToListCustomObject<T>(resultContent);

            return result;
        }

        public async static Task<int> ResponsePostToObject(HttpResponseMessage? httpResponseMessage)
        {
            if(!ResponseWasOk(httpResponseMessage))
                return 0;

            string resultContent = await httpResponseMessage.Content.ReadAsStringAsync();

            var result = int.Parse(resultContent);

            return result;
        }

        public async static Task<int> ResponsePutToObject(HttpResponseMessage? httpResponseMessage)
        {
            if(!ResponseWasOk(httpResponseMessage))
                return 0;

            string resultContent = await httpResponseMessage.Content.ReadAsStringAsync();

            var result = int.Parse(resultContent);

            return result;
        }

        public async static Task<int> ResponseDeleteToObject(HttpResponseMessage? httpResponseMessage)
        {
            if(!ResponseWasOk(httpResponseMessage))
                return 0;

            string resultContent = await httpResponseMessage.Content.ReadAsStringAsync();

            var result = int.Parse(resultContent);

            return result;
        }

        private static bool ResponseWasOk(HttpResponseMessage? httpResponseMessage)
        {
            try
            {
                if(httpResponseMessage == null)
                    throw new ArgumentNullException(nameof(httpResponseMessage));

                if(!httpResponseMessage.IsSuccessStatusCode)
                {
                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
