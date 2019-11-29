using Newtonsoft.Json;
using System;

namespace Hawk.API.Models.Pullrequest
{
    public class PullRequests
    {
        [JsonProperty("repository")]
        public Repository Repository { get; set; }

        [JsonProperty("pullRequestId")]
        public int PullRequestId { get; set; }

        [JsonProperty("codeReviewId")]
        public int CodeReviewId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("createdBy")]
        public CreatedBy CreatedBy { get; set; }

        [JsonProperty("creationDate")]
        public DateTime CreationDate { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("sourceRefName")]
        public string SourceRefName { get; set; }

        [JsonProperty("targetRefName")]
        public string TargetRefName { get; set; }

        [JsonProperty("mergeStatus")]
        public string MergeStatus { get; set; }

        [JsonProperty("isDraft")]
        public bool IsDraft { get; set; }

        [JsonProperty("mergeId")]
        public string MergeId { get; set; }

        [JsonProperty("lastMergeSourceCommit")]
        public LastMergeCommit LastMergeSourceCommit { get; set; }

        [JsonProperty("lastMergeTargetCommit")]
        public LastMergeCommit LastMergeTargetCommit { get; set; }

        [JsonProperty("lastMergeCommit")]
        public LastMergeCommit LastMergeCommit { get; set; }

        [JsonProperty("reviewers")]
        public Reviewer[] Reviewers { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("supportsIterations")]
        public bool SupportsIterations { get; set; }
    }
}
