using GenericApiHandler.Data.Enums;
using System.Net.Http.Json;

namespace CryptoAlertsBot.ApiHandler
{
    public static class ApiCalls
    {
        public static async Task<HttpResponseMessage?> ExeCall(ApiCallTypesEnum callType, string uri, object obj = default, string? baseAddress = default, string schema = default, string apiToken = default)
        {
            try
            {
                if (obj == default && (callType == ApiCallTypesEnum.Post || callType == ApiCallTypesEnum.Put))
                {
                    return default;
                }

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseAddress ?? ApiAppSettingsManager.GetApiBaseAddress());

                    client.DefaultRequestHeaders.Add("schema", schema ?? ApiAppSettingsManager.GetApiDefaultSchema());

                    if (apiToken != default)
                    {
                        client.DefaultRequestHeaders.Add("Authorization", apiToken);
                    }

                    switch (callType)
                    {
                        case ApiCallTypesEnum.Get:
                            return await client.GetAsync(uri);
                        case ApiCallTypesEnum.Post:
                            return await client.PostAsJsonAsync(uri, obj);
                        case ApiCallTypesEnum.Put:
                            return await client.PutAsJsonAsync(uri, obj);
                        case ApiCallTypesEnum.Delete:
                            return await client.DeleteAsync(uri);
                        default:
                            return default;
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
