using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Hawk.API.Helpers
{
    public static class HttpRequestExtensions
    {
        public static async Task<T> ReadAs<T>(this HttpRequest req)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var result = JsonConvert.DeserializeObject<T>(requestBody);
            return result;
        }

        public static async Task<(T value, bool success, string errormessage)> CallApiAsync<T>(IHttpClientFactory httpClient, string accessToken, string url) where T : class
        {
            try
            {
                var client = httpClient.CreateClient(nameof(T));
                client.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(
                        Encoding.UTF8.GetBytes(
                            string.Format("{0}:{1}", "", accessToken))));

                var response = await client.GetAsync(url);

                return response.StatusCode == System.Net.HttpStatusCode.OK
                    ? (await response.Content.ReadAsAsync<T>(), true, string.Empty)
                    : (null, false, $"Http error occured {response.StatusCode} ({response.ReasonPhrase}).");
            }
            catch (Exception ex)
            {
                return (null, false, $"Unhandled exception occured:\r\n{ex}.");
            }
        }
    }
}
