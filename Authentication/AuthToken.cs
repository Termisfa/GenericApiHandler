using CryptoAlertsBot;
using CryptoAlertsBot.ApiHandler;
using CryptoAlertsBot.ApiHandler.Helpers;
using GenericApiHandler.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericApiHandler.Authentication
{
    public class AuthToken
    {
        public string Token { get; set; }

        private readonly LogEvent _logEvent;

        public AuthToken(LogEvent logEvent)
        {
            _logEvent = logEvent;
        }

        public async Task InitializeAsync()
        {
            try
            {
                await RefreshToken();

                var timer = new System.Timers.Timer(1000 * 60 * 60 * 1); //It should be 1000 * 60 * 60 * 1  (1 hour)
                timer.Start();
                timer.Elapsed += TimerCallbackAsync;
            }
            catch (Exception e)
            {
                _logEvent.Log(exc: e);
            }
        }

        public async Task RefreshToken()
        {
            try
            {
                AuthenticateRequest authReq = new()
                {
                    Username = ApiAppSettingsManager.GetSchemaUserName(),
                    Password = ApiAppSettingsManager.GetSchemaUserPassw(),
                    Schema = ApiAppSettingsManager.GetApiDefaultSchema()
                };

                string uri = "/Auth/Authenticate";

                var httpResponse = await ApiCalls.ExeCall(ApiCallTypesEnum.Post, uri, authReq);

                AuthenticateResponse response = HttpResponseHandler.GetResponseFromAuthAsync(httpResponse).Result;

                if (response == null)
                {
                    string customError = httpResponse.Content.ReadAsStringAsync().Result;
                    if(string.IsNullOrEmpty(customError))
                    {
                        throw new Exception($"Error in Refresh Token. \n Status code: { httpResponse.StatusCode }. Reason: { httpResponse.ReasonPhrase}. \n Stacktrace: \n {httpResponse.RequestMessage.ToString()}");
                    }
                    else
                    {
                        throw new Exception(customError);
                    }
                }

                Token = response.Token;
            }
            catch (Exception e)
            {
                _logEvent.Log(exc: e);
            }
        }

        private void TimerCallbackAsync(object? sender, System.Timers.ElapsedEventArgs elapsed)
        {
            try
            {
                RefreshToken();
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
