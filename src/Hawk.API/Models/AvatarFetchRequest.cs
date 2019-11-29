using System;

namespace Hawk.API.Models
{
    public class AvatarFetchRequest
    {
        public string AccessToken { get; set; }
        public Uri Uri { get; set; }

        public string ImageId { get; set; }
    }
}