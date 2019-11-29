using System.Collections.Generic;

namespace Hawk.API
{
    public class ResponseOrganization
    {
        public string Id { get; }
        public string OrganizationName { get; }
        public bool Success { get; }
        public ResponseRepository[] Repositories { get; }

        public ResponseOrganization(string organizationName, bool success, ResponseRepository[] repositories)
        {
            Id = organizationName?.ToLowerInvariant();
            OrganizationName = organizationName;
            Success = success;
            Repositories = repositories;
        }

        public override bool Equals(object obj)
        {
            return obj is ResponseOrganization other &&
                   OrganizationName == other.OrganizationName &&
                   EqualityComparer<ResponseRepository[]>.Default.Equals(Repositories, other.Repositories);
        }

        public override int GetHashCode()
        {
            var hashCode = 88129986;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(OrganizationName);
            hashCode = hashCode * -1521134295 + EqualityComparer<ResponseRepository[]>.Default.GetHashCode(Repositories);
            return hashCode;
        }
    }
}
