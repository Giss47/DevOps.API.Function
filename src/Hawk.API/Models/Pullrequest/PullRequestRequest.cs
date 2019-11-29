using Newtonsoft.Json;

namespace Hawk.API.Models.Pullrequest
{
    public class PullRequestRequest
    {
        [JsonProperty("organizations")]
        public Organization[] Organizations { get; set; }
    }
}
