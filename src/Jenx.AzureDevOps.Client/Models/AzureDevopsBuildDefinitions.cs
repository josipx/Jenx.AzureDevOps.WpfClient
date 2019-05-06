using Newtonsoft.Json;

namespace Jenx.AzureDevOps.Client.Models
{
    public class AzureDevOpsBuildDefinitions
    {
        [JsonProperty("count")]
        public int NoOfBuildDefinitoins { get; set; }

        [JsonProperty("value")]
        public Value[] BuildDefinitions { get; set; }
    }
}