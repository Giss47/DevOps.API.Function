using System.Collections.Generic;

namespace Hawk.API
{
    public class GetPullRequestsResponse
    {
        public ResponseOrganization[] Organizations { get; }

        public GetPullRequestsResponse(ResponseOrganization[] organizations)
        {
            Organizations = organizations;
        }

        public override bool Equals(object obj)
        {
            return obj is GetPullRequestsResponse other &&
                   EqualityComparer<ResponseOrganization[]>.Default.Equals(Organizations, other.Organizations);
        }

        public override int GetHashCode()
        {
            return 39295411 + EqualityComparer<ResponseOrganization[]>.Default.GetHashCode(Organizations);
        }
    }
}
