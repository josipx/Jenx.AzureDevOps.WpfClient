namespace Jenx.AzureDevOps.WpfClient.Settings
{
    public interface ISettings
    {
        string PersonalAccessToken { get; set; }
        string OrganizationName { get; set; }

        void Store();

        void Load();
    }
}