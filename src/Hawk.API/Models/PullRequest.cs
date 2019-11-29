using Hawk.API.Models.Pullrequest;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Hawk.API.Models
{
    class PullRequest
    {
        [JsonProperty("value")]
        public List<PullRequests> PullRequests { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        public string NameOfRepo { get; set; }
        public Organization Organization { get; set; }
    }
}
