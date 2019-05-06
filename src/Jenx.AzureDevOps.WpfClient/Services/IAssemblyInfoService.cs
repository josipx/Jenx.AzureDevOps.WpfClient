using System;

namespace Jenx.AzureDevOps.WpfClient.Services
{
    public interface IAssemblyInfoService
    {
        string Company { get; }

        string Product { get; }

        string Copyright { get; }

        string Trademark { get; }

        string Description { get; }

        string VersionFull { get; }

        Version Version { get; }
    }
}