using Newtonsoft.Json;
using System;

namespace Hawk.API.Models.Pullrequest
{
    public class LastMergeCommit
    {
        [JsonProperty("commitId")]
        public string CommitId { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }
    }
}
