using GenericApiHandler.Data.Enums;
using System.Net.Http.Json;

namespace CryptoAlertsBot.ApiHandler
{
    public static class ApiCalls
    {
        public static async Task<HttpResponseMessage?> ExeCall(ApiCallTypesEnum callType, string uri, object obj = default, string? baseAddress = default)
        {
            try
            {
                if (obj == default && (callType == ApiCallTypesEnum.Post || callType == ApiCallTypesEnum.Put))
                    return default;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseAddress ?? ApiAppSettingsManager.GetApiBaseAddress());

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





        //public static async Task<HttpResponseMessage?> Get(string uri, string? baseAddress = default)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri(baseAddress ?? ApiAppSettingsManager.GetApiBaseAddress());

        //        var result = await client.GetAsync(uri);

        //        return result;
        //    }
        //}

        //public static async Task<HttpResponseMessage?> Post(string uri, object obj, string? baseAddress = default)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri(baseAddress ?? ApiAppSettingsManager.GetApiBaseAddress());

        //        var result = await client.PostAsJsonAsync(uri, obj);

        //        return result;
        //    }
        //}

        //public static async Task<HttpResponseMessage?> Put(string uri, object obj, string? baseAddress = default)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri(baseAddress ?? ApiAppSettingsManager.GetApiBaseAddress());

        //        var result = await client.PutAsJsonAsync(uri, obj);

        //        return result;
        //    }
        //}

        //public static async Task<HttpResponseMessage?> Delete(string uri, string? baseAddress = default)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri(baseAddress ?? ApiAppSettingsManager.GetApiBaseAddress());

        //        var result = await client.DeleteAsync(uri);

        //        return result;
        //    }
        //}
    }
}
