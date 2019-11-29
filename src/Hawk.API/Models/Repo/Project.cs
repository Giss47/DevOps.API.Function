using Newtonsoft.Json;
using System;

namespace Hawk.API.Models.Repo
{
    public class Project
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("revision")]
        public long Revision { get; set; }

        [JsonProperty("visibility")]
        public string Visibility { get; set; }

        [JsonProperty("lastUpdateTime")]
        public DateTimeOffset LastUpdateTime { get; set; }
    }
}
