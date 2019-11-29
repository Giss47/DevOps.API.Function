using Hawk.API.Models.Repo;
using Newtonsoft.Json;

namespace Hawk.API.Models
{
    public class RepositoryResponse
    {
        [JsonProperty("value")]
        public Repository[] Repositories { get; set; }

        [JsonProperty("count")]
        public long Count { get; set; }
    }
}
