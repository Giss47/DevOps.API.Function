using Newtonsoft.Json;
using System.Collections.Generic;

namespace Hawk.API.Models
{
    public class Organization
    {


        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        internal bool Success { get; set; }

        internal List<Repo.Repository> Repositories { get; } = new List<Repo.Repository>();
    }
}
