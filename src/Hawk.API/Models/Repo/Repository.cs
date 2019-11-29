using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Hawk.API.Models.Repo
{
    public class Repository
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("project")]
        public Project Project { get; set; }

        [JsonProperty("defaultBranch")]
        public string DefaultBranch { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("remoteUrl")]
        public Uri RemoteUrl { get; set; }

        [JsonProperty("sshUrl")]
        public string SshUrl { get; set; }

        [JsonProperty("webUrl")]
        public Uri WebUrl { get; set; }

        internal List<Pullrequest.PullRequests> PullRequests { get; } = new List<Pullrequest.PullRequests>();
    }
}
