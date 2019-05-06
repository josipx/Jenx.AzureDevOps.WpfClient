using Jenx.AzureDevOps.Client.Models;
using System.Threading.Tasks;

namespace Jenx.AzureDevOps.Client
{
    public interface IAzureDevOpsService
    {
        Task<AzureDevOpsProjects> GetProjectsAsync();

        Task<AzureDevOpsRepos> GetReposAsync(string projectName);

        Task<AzureDevOpsBuilds> GetProjectBuildsAsync(string projectName);

        Task<BuildTriggeredResult> TriggerProjectBuildAsync(string projectName, int definitionId);

        Task<AzureDevOpsBuildDefinitions> GetProjectBuildDefinitionsAsync(string projectName);
    }
}