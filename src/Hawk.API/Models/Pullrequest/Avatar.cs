using Newtonsoft.Json;
using System;

namespace Hawk.API.Models.Pullrequest
{
    public class Avatar
    {
        [JsonProperty("href")]
        public Uri Href { get; set; }
    }
}
