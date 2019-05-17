using Jenx.AzureDevOps.Client.Models;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Jenx.AzureDevOps.Client
{
    public class AzureDevOpsService : IAzureDevOpsService
    {
        private readonly IAzureDevOpsSettings _settings;

        public AzureDevOpsService(IAzureDevOpsSettings settings)
        {
            _settings = settings;
        }

        public async Task<AzureDevOpsProjects> GetProjectsAsync()
        {
            var azureDevOpsProjects = new AzureDevOpsProjects();

            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                        Convert.ToBase64String(Encoding.ASCII.GetBytes($":{_settings.PersonalAccessToken}")));

                    using (var response = await client.GetAsync($"https://dev.azure.com/{_settings.OrganizationName}/_apis/projects"))
                    {
                        response.EnsureSuccessStatusCode();
                        var responseBody = await response.Content.ReadAsStringAsync();
                        azureDevOpsProjects = JsonConvert.DeserializeObject<AzureDevOpsProjects>(responseBody);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            return azureDevOpsProjects;
        }

        public async Task<AzureDevOpsRepos> GetReposAsync(string projectName)
        {
            var azureDevOpsRepos = new AzureDevOpsRepos();

            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                        Convert.ToBase64String(Encoding.ASCII.GetBytes($":{_settings.PersonalAccessToken}")));

                    using (var response = await client.GetAsync($"https://dev.azure.com/{_settings.OrganizationName}/{projectName}/_apis/git/repositories"))
                    {
                        response.EnsureSuccessStatusCode();
                        var responseBody = await response.Content.ReadAsStringAsync();
                        azureDevOpsRepos = JsonConvert.DeserializeObject<AzureDevOpsRepos>(responseBody);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            return azureDevOpsRepos;
        }

        public async Task<AzureDevOpsBuilds> GetProjectBuildsAsync(string projectName)
        {
            var azureDevOpsBuilds = new AzureDevOpsBuilds();

            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                        Convert.ToBase64String(Encoding.ASCII.GetBytes($":{_settings.PersonalAccessToken}")));

                    using (var response = await client.GetAsync($"https://dev.azure.com/{_settings.OrganizationName}/{projectName}/_apis/build/builds"))
                    {
                        response.EnsureSuccessStatusCode();
                        var responseBody = await response.Content.ReadAsStringAsync();
                        azureDevOpsBuilds = JsonConvert.DeserializeObject<AzureDevOpsBuilds>(responseBody);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            return azureDevOpsBuilds;
        }

        public async Task<BuildTriggeredResult> TriggerProjectBuildAsync(string projectName, int definitionId)
        {
            var AzureDevOpsBuildTrigger = new BuildTriggeredResult();

            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                        Convert.ToBase64String(Encoding.ASCII.GetBytes($":{_settings.PersonalAccessToken}")));

                    var json = "{\"definition\": {\"id\": " + definitionId + "}}";
                    var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                    using (var response = await client.PostAsync($"https://dev.azure.com/{_settings.OrganizationName}/{projectName}/_apis/build/builds?api-version=5.0", stringContent))
                    {
                        response.EnsureSuccessStatusCode();
                        var responseBody = await response.Content.ReadAsStringAsync();
                        AzureDevOpsBuildTrigger = JsonConvert.DeserializeObject<BuildTriggeredResult>(responseBody);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            return AzureDevOpsBuildTrigger;
        }

        public async Task<AzureDevOpsBuildDefinitions> GetProjectBuildDefinitionsAsync(string projectName)
        {
            var azureDevOpsBuildDefinitions = new AzureDevOpsBuildDefinitions();

            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                        Convert.ToBase64String(Encoding.ASCII.GetBytes($":{_settings.PersonalAccessToken}")));

                    using (var response = await client.GetAsync($"https://dev.azure.com/{_settings.OrganizationName}/{projectName}/_apis/build/definitions"))
                    {
                        response.EnsureSuccessStatusCode();
                        var responseBody = await response.Content.ReadAsStringAsync();
                        azureDevOpsBuildDefinitions = JsonConvert.DeserializeObject<AzureDevOpsBuildDefinitions>(responseBody);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            return azureDevOpsBuildDefinitions;
        }
    }
}