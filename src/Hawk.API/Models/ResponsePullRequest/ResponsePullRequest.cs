using System;
using System.Collections.Generic;

namespace Hawk.API
{
    public class ResponsePullRequest
    {
        public string Title { get; }
        public string Description { get; }
        public string DisplayName { get; }
        public string Url { get; }
        public Uri  ImageUrl { get; set; }
        public int ID { get; set; }

        public ResponsePullRequest(string title, string description, string displayName, string url, Uri imageUrl, int id)
        {
            Title = title;
            Description = description;
            DisplayName = displayName;
            Url = url;
            ImageUrl = imageUrl;
            ID = id;
        }

        public override bool Equals(object obj)
        {
            return obj is ResponsePullRequest other &&
                   Title == other.Title &&
                   Description == other.Description &&
                   DisplayName == other.DisplayName &&
                   Url == other.Url;
        }

        public override int GetHashCode()
        {
            var hashCode = -1721615202;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Title);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Description);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(DisplayName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Url);
            return hashCode;
        }
    }
}
