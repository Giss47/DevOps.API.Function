using Newtonsoft.Json;
using System;

namespace Hawk.API.Models.Pullrequest
{
    public class Project
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("visibility")]
        public string Visibility { get; set; }

        [JsonProperty("lastUpdateTime")]
        public DateTime LastUpdateTime { get; set; }
    }
}
