namespace Jenx.AzureDevOps.Client
{
    public interface IAzureDevOpsSettings
    {
        string PersonalAccessToken { get; set; }
        string OrganizationName { get; set; }
    }
}