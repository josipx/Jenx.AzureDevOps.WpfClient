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
            var AzureDevOpsProjects = new AzureDevOpsProjects();

            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                        Convert.ToBase64String(Encoding.ASCII.GetBytes($":{_settings.PersonalAccessToken}")));

                    using (HttpResponseMessage response = await client.GetAsync($"https://dev.azure.com/{_settings.OrganizationName}/_apis/projects"))
                    {
                        response.EnsureSuccessStatusCode();
                        string responseBody = await response.Content.ReadAsStringAsync();
                        AzureDevOpsProjects = JsonConvert.DeserializeObject<AzureDevOpsProjects>(responseBody);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            return AzureDevOpsProjects;
        }

        public async Task<AzureDevOpsRepos> GetReposAsync(string projectName)
        {
            var AzureDevOpsRepos = new AzureDevOpsRepos();

            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                        Convert.ToBase64String(Encoding.ASCII.GetBytes($":{_settings.PersonalAccessToken}")));

                    using (HttpResponseMessage response = await client.GetAsync($"https://dev.azure.com/{_settings.OrganizationName}/{projectName}/_apis/git/repositories"))
                    {
                        response.EnsureSuccessStatusCode();
                        string responseBody = await response.Content.ReadAsStringAsync();
                        AzureDevOpsRepos = JsonConvert.DeserializeObject<AzureDevOpsRepos>(responseBody);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            return AzureDevOpsRepos;
        }

        public async Task<AzureDevOpsBuilds> GetProjectBuildsAsync(string projectName)
        {
            var AzureDevOpsBuilds = new AzureDevOpsBuilds();

            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                        Convert.ToBase64String(Encoding.ASCII.GetBytes($":{_settings.PersonalAccessToken}")));

                    using (HttpResponseMessage response = await client.GetAsync($"https://dev.azure.com/{_settings.OrganizationName}/{projectName}/_apis/build/builds"))
                    {
                        response.EnsureSuccessStatusCode();
                        string responseBody = await response.Content.ReadAsStringAsync();
                        AzureDevOpsBuilds = JsonConvert.DeserializeObject<AzureDevOpsBuilds>(responseBody);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            return AzureDevOpsBuilds;
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

                    using (HttpResponseMessage response = await client.PostAsync($"https://dev.azure.com/{_settings.OrganizationName}/{projectName}/_apis/build/builds?api-version=5.0", stringContent))
                    {
                        response.EnsureSuccessStatusCode();
                        string responseBody = await response.Content.ReadAsStringAsync();
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
            var AzureDevOpsBuildDefinitions = new AzureDevOpsBuildDefinitions();

            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                        Convert.ToBase64String(Encoding.ASCII.GetBytes($":{_settings.PersonalAccessToken}")));

                    using (HttpResponseMessage response = await client.GetAsync($"https://dev.azure.com/{_settings.OrganizationName}/{projectName}/_apis/build/definitions"))
                    {
                        response.EnsureSuccessStatusCode();
                        string responseBody = await response.Content.ReadAsStringAsync();
                        AzureDevOpsBuildDefinitions = JsonConvert.DeserializeObject<AzureDevOpsBuildDefinitions>(responseBody);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            return AzureDevOpsBuildDefinitions;
        }
    }
}