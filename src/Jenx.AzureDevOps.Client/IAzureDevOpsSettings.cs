namespace Jenx.AzureDevOps.Client
{
    public interface IAzureDevOpsSettings
    {
        string PersonalAccessToken { get; set; }
        string OrganizationName { get; set; }
    }

    public class AzureDevOpsSettings : IAzureDevOpsSettings
    {
        public string PersonalAccessToken { get; set; }

        public string OrganizationName { get; set; }
    }
}