using CryptoAlertsBot.ApiHandler.Models;
using GenericApiHandler.Authentication;
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

                if (ResponseWasOk(httpResponseMessage))
                {
                    string responseString = await httpResponseMessage.Content.ReadAsStringAsync();
                    response = JsonConvert.DeserializeObject<Response>(responseString);
                }
                else
                {
                    response = new()
                    {
                        Result = string.Empty,
                        Success = false,
                        ErrorInfo = new()
                        {
                            Message = $"Status code: {httpResponseMessage.StatusCode}. Reason: {httpResponseMessage.ReasonPhrase}",
                            StackTrace = httpResponseMessage.RequestMessage.ToString()
                        }
                    };
                }

                return response;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public static async Task<AuthenticateResponse> GetResponseFromAuthAsync(HttpResponseMessage? httpResponseMessage)
        {
            try
            {
                AuthenticateResponse response = default;

                if (ResponseWasOk(httpResponseMessage))
                {
                    string responseString = await httpResponseMessage.Content.ReadAsStringAsync();
                    response = JsonConvert.DeserializeObject<AuthenticateResponse>(responseString);
                }

                return response;
            }
            catch (Exception e)
            {
                throw;
            }
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
