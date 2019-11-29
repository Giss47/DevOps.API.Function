using System.Collections.Generic;

namespace Hawk.API
{
    public class ResponseRepository
    {
        public string Id { get; }
        public string Name { get; }
        public ResponsePullRequest[] PullRequests { get; }

        public ResponseRepository(string id, string name, ResponsePullRequest[] pullRequests)
        {
            Id = id;
            Name = name;
            PullRequests = pullRequests;
        }

        public override bool Equals(object obj)
        {
            return obj is ResponseRepository other &&
                   Id == other.Id &&
                   Name == other.Name &&
                   EqualityComparer<ResponsePullRequest[]>.Default.Equals(PullRequests, other.PullRequests);
        }

        public override int GetHashCode()
        {
            var hashCode = -1044364298;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Id);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<ResponsePullRequest[]>.Default.GetHashCode(PullRequests);
            return hashCode;
        }
    }
}
