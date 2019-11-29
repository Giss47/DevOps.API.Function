using Microsoft.WindowsAzure.Storage.Table;

namespace Hawk.API.Models
{
    public class Session : TableEntity
    {
        public string Organizations { get; set; }

        public Session()
        { }

        public Session(string partitionKey, string rowKey, string organizations = null) : base(partitionKey, rowKey)
        {
            Organizations = organizations;
        }
    }
}
