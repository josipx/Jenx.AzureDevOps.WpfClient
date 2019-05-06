using Newtonsoft.Json;

namespace Jenx.AzureDevOps.Client.Models
{
    public class AzureDevOpsBuilds
    {
        [JsonProperty("count")]
        public int NoOfBuilds { get; set; }

        [JsonProperty("value")]
        public Build[] Builds { get; set; }
    }
}