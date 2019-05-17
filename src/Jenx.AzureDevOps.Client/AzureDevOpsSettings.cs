namespace Jenx.AzureDevOps.Client
{
    public class AzureDevOpsSettings : IAzureDevOpsSettings
    {
        public string PersonalAccessToken { get; set; }

        public string OrganizationName { get; set; }
    }
}