using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Hawk.API.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Hawk.API
{
    public class AvatarFetch
    {
        private IHttpClientFactory HttpClientFactory { get; }

        public AvatarFetch(IHttpClientFactory httpClientFactory)
            => HttpClientFactory = httpClientFactory;
        [FunctionName(nameof(AvatarFetch))]
        public async Task Run(
            [QueueTrigger(nameof(AvatarFetch))]AvatarFetchRequest avatarFetchRequest,
            [Blob("avatars")] CloudBlobContainer container,
            ILogger log
            )
        {
            if (string.IsNullOrWhiteSpace(avatarFetchRequest?.ImageId))
            {
                log.LogWarning("Invalid Image Id.");
                return;
            }


            if (string.IsNullOrWhiteSpace(avatarFetchRequest?.AccessToken))
            {
                log.LogWarning("Invalid Access Token.");
                return;
            }

            if (avatarFetchRequest.Uri == null)
            {
                log.LogWarning("Invalid Uri.");
                return;
            }

            await container.CreateIfNotExistsAsync();

            var blob = container.GetBlockBlobReference(avatarFetchRequest.ImageId);


            if (await blob.ExistsAsync())
            {
                return;
            }

            var client = HttpClientFactory.CreateClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                 Convert.ToBase64String(
                     Encoding.UTF8.GetBytes(
                         string.Format("{0}:{1}", "", avatarFetchRequest.AccessToken))));


            using (var response = await client.GetAsync(avatarFetchRequest.Uri))
            {
                response.EnsureSuccessStatusCode();

                blob.Properties.ContentType = response.Content.Headers.ContentType.MediaType;

                using (var target = await blob.OpenWriteAsync())
                {
                    await response.Content.CopyToAsync(target);
                }
            }
        }
    }
}
