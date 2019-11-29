using Hawk.API.Helpers;
using Hawk.API.Models;
using Hawk.API.Models.Pullrequest;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Hawk.API
{
    public class GetPullRequests
    {
        private IHttpClientFactory HttpClientFactory { get; }

        public GetPullRequests(IHttpClientFactory httpClientFactory)
            => HttpClientFactory = httpClientFactory;

        [FunctionName(nameof(GetPullRequests))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "getpullrequests")] HttpRequest req,
            [Table("Session")]  IAsyncCollector<Session> sessionCollector,
            [Queue(nameof(AvatarFetch))] ICollector<AvatarFetchRequest> avatarCollector,
            ILogger log)
        {
            PullRequestRequest pullRequestRequest;
            var apiBaseurl = new Uri(string.Concat(req.IsHttps ? "https://" : "http://", req.Host, "/api/avatar/"), UriKind.Absolute);
            try
            {
                pullRequestRequest = await req.ReadAs<PullRequestRequest>();
            }
            catch (Exception ex)
            {
                log.LogError(ex, "Failed to parse request.");
                pullRequestRequest = null;
            }

            if (pullRequestRequest == null|| pullRequestRequest.Organizations==null)
            {
                return new BadRequestResult();
            }

            if (pullRequestRequest.Organizations?.Any() == false)
            {
                return new NotFoundResult();
            }


            var session = new Session("Session", Guid.NewGuid().ToString("d"), string.Join(",", pullRequestRequest.Organizations.Select(org => org.Name)));
            await sessionCollector.AddAsync(session);

            var getRepositoriesTasks = pullRequestRequest
                    .Organizations
                    .Where(org => org.Name != null || org.AccessToken != null)
                    .Select(organization => Task.Run(async () =>
                    {
                        var (value, success, errormessage) = await Helpers.HttpRequestExtensions.CallApiAsync<RepositoryResponse>(
                            HttpClientFactory,
                            organization.AccessToken,
                            $"https://dev.azure.com/{Uri.EscapeDataString(organization.Name)}/_apis/git/repositories?api-version=5.0"
                            );

                        if (organization.Success = success)
                        {
                            organization.Repositories.AddRange(value.Repositories);
                        }

                        return (id: organization.Name, success, errormessage);
                    }))
                    .ToArray();

            await Task.WhenAll(getRepositoriesTasks);

            getRepositoriesTasks.LogAnyErrors(log);

            var getPullRequestTasks = (
                 from organization in pullRequestRequest.Organizations
                 where organization.Success
                 from repository in organization.Repositories
                 select Task.Run(async () =>
                 {
                     string url = $"https://dev.azure.com/{Uri.EscapeDataString(organization.Name)}/_apis/git/repositories/{Uri.EscapeDataString(repository.Id)}/pullrequests?api-version=5.0";
                     var (value, success, errormessage) = await Helpers.HttpRequestExtensions.CallApiAsync<PullRequest>(HttpClientFactory, organization.AccessToken, url);

                     if (value.Count > 0 && value.PullRequests?.Any() == true)
                     {
                         repository.PullRequests.AddRange(value.PullRequests.Select(
                             pr =>
                             {
                                 var imageId = Uri.EscapeDataString(Convert.ToBase64String(Encoding.UTF8.GetBytes(pr.CreatedBy.ImageUrl.PathAndQuery)));

                                 avatarCollector.Add(new AvatarFetchRequest
                                 {
                                     AccessToken = organization.AccessToken,
                                     Uri = pr.CreatedBy.ImageUrl,
                                     ImageId = imageId
                                 });

                                 pr.CreatedBy.ImageUrl = new Uri (apiBaseurl, string.Concat(Uri.EscapeDataString(session.RowKey), "/", Uri.EscapeDataString(imageId)));

                                 return pr;
                             })
                             );
                     }
                     return (id: repository.Name, success, errormessage);
                 })).ToArray();

            await Task.WhenAll(
               getPullRequestTasks
            );

            getPullRequestTasks.LogAnyErrors(log);

            return new OkObjectResult(
                new GetPullRequestsResponse(
                    pullRequestRequest.ToResponseOrganizations()
                )
            );
        }
    }
}
