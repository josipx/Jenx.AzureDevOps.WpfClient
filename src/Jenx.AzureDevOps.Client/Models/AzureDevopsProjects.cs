using Newtonsoft.Json;

namespace Jenx.AzureDevOps.Client.Models
{
    public class AzureDevOpsProjects
    {
        [JsonProperty("count")]
        public int NoOfProjects { get; set; }

        [JsonProperty("value")]
        public Project[] Projects { get; set; }
    }
}