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
                if (obj == default && (callType == ApiCallTypesEnum.Post || callType == ApiCallTypesEnum.Put || callType == ApiCallTypesEnum.BulkInsert))
                {
                    return default;
                }

                using var client = new HttpClient();
                client.BaseAddress = new Uri(baseAddress ?? ApiAppSettingsManager.GetApiBaseAddress());

                client.DefaultRequestHeaders.Add("schema", schema ?? ApiAppSettingsManager.GetApiDefaultSchema());

                if (apiToken != default)
                {
                    client.DefaultRequestHeaders.Add("Authorization", apiToken);
                }

                return callType switch
                {
                    ApiCallTypesEnum.Get => await client.GetAsync(uri),
                    ApiCallTypesEnum.Post or ApiCallTypesEnum.BulkInsert => await client.PostAsJsonAsync(uri, obj),
                    ApiCallTypesEnum.Put => await client.PutAsJsonAsync(uri, obj),
                    ApiCallTypesEnum.Delete => await client.DeleteAsync(uri),
                    _ => default,
                };
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
