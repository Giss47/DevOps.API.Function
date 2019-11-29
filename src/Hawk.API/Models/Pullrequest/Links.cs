using Newtonsoft.Json;

namespace Hawk.API.Models.Pullrequest
{
    public class Links
    {
        [JsonProperty("avatar")]
        public Avatar Avatar { get; set; }
    }
}
