using Newtonsoft.Json;
using System;

namespace Hawk.API.Models.Pullrequest
{
    public class Reviewer
    {
        [JsonProperty("reviewerUrl")]
        public Uri ReviewerUrl { get; set; }

        [JsonProperty("vote")]
        public long Vote { get; set; }

        [JsonProperty("isFlagged")]
        public bool IsFlagged { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("_links")]
        public Links Links { get; set; }

        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("uniqueName")]
        public string UniqueName { get; set; }

        [JsonProperty("imageUrl")]
        public Uri ImageUrl { get; set; }
    }
}
