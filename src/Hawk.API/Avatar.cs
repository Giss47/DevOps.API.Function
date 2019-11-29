using Hawk.API.Helpers;
using Hawk.API.Models;
using Hawk.API.Models.Pullrequest;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Hawk.API
{
    public class Avatar
    {
        [FunctionName(nameof(Avatar))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "avatar/{sessionId}/{imageId}")] HttpRequest req,
            [Table("Session", "Session", "{sessionId}")]  Session session,
            [Blob("avatars")] CloudBlobContainer container,
            ILogger log,
            string imageId)
        {
            try
            {
                if (session == null || session.Timestamp.AddHours(1) < DateTimeOffset.UtcNow)
                {
                    return Shields.AvatarDeniedResult;
                }

                await container.CreateIfNotExistsAsync();


                var blob = container.GetBlockBlobReference(imageId);

                if (!await blob.ExistsAsync())
                {
                    return Shields.AvatarNotFoundResult;
                }


                var accessSignature = blob.GetSharedAccessSignature(
                    new SharedAccessBlobPolicy
                    {
                        Permissions = SharedAccessBlobPermissions.Read,
                        SharedAccessStartTime = DateTimeOffset.UtcNow,
                        SharedAccessExpiryTime = DateTimeOffset.UtcNow.AddHours(2)
                    });


                var url = string.Concat(
                    blob.Uri,
                    accessSignature
                    );

                return new RedirectResult(url);
            }
            catch (Exception ex)
            {
                log.LogError(ex, "Unhandled Avatar Exception.");
                return Shields.AvatarUnhandledExeptionResult;
            }
        }
    }
}
