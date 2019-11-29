using Hawk.API.Models;
using Hawk.API.Models.Pullrequest;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hawk.API
{
    public static class GetPullRequestResponseExtensions
    {
        public static ResponsePullRequest[] ToResponsePullRequest(this ICollection<PullRequests> pullRequests, string organizationName, string repositoryName, string projectName)
        {
            return pullRequests
                    .Select(
                        pullrequest => new ResponsePullRequest(
                        pullrequest.Title,
                        pullrequest.Description,
                        pullrequest.CreatedBy?.DisplayName,
                        $"https://dev.azure.com/{Uri.EscapeDataString(organizationName)}/{Uri.EscapeDataString(projectName)}/{Uri.EscapeDataString(repositoryName)}/_git/{Uri.EscapeDataString(repositoryName)}/pullrequests?_a=mine",
                        pullrequest.CreatedBy?.ImageUrl,
                        pullrequest.PullRequestId
                    )).ToArray();
        }

        public static ResponseRepository[] ToResponseRepositories(this Organization organization)
        {
            return organization
                    .Repositories
                    .Select(repository => new ResponseRepository(
                                            repository.Id,
                                            repository.Name,
                                            repository.PullRequests
                                                .ToResponsePullRequest(
                                                    organization.Name,
                                                    repository.Name, repository.Project.Name)
                        )
                    ).ToArray();
        }

        public static ResponseOrganization[] ToResponseOrganizations(this PullRequestRequest pullRequestRequest)
        {
            return pullRequestRequest
                    .Organizations
                    .Select(
                        organization => new ResponseOrganization(
                        organization.Name,
                        organization.Success,
                        organization.ToResponseRepositories()
                        )
                    ).ToArray();
        }
    }
}
