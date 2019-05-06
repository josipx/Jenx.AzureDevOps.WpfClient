using Newtonsoft.Json;

namespace Jenx.AzureDevOps.Client.Models
{
    public class AzureDevOpsRepos
    {
        [JsonProperty("value")]
        public Repos[] Repos { get; set; }

        [JsonProperty("count")]
        public int NoOfRepos { get; set; }
    }
}