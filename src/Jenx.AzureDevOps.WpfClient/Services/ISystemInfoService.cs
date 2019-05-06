namespace Jenx.AzureDevOps.WpfClient.Services
{
    public interface ISystemInfoService
    {
        string ProductName { get; }

        string ProductVersion { get; }

        string ProductCopyright { get; }

        string ProductDescription { get; }

        string ProductCompany { get; }

        string OperatingSystemInfo { get; }
    }
}