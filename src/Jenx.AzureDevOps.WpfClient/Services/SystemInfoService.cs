using System;
using System.Management;
using System.Text;

namespace Jenx.AzureDevOps.WpfClient.Services
{
    public class SystemInfoService : ISystemInfoService
    {
        private readonly IAssemblyInfoService _assemblyInfoService;

        public SystemInfoService(IAssemblyInfoService assemblyInfoService)
        {
            _assemblyInfoService = assemblyInfoService;
        }

        public string ProductVersion
        {
            get
            {
                try
                {
                    return _assemblyInfoService.VersionFull;
                }
                catch
                {
                    return "n/a";
                }
            }
        }

        public string ProductCopyright
        {
            get
            {
                try
                {
                    return _assemblyInfoService.Copyright;
                }
                catch
                {
                    return "n/a";
                }
            }
        }

        public string ProductName
        {
            get
            {
                try
                {
                    return _assemblyInfoService.Product;
                }
                catch
                {
                    return "n/a";
                }
            }
        }

        public string ProductDescription
        {
            get
            {
                try
                {
                    return _assemblyInfoService.Description;
                }
                catch
                {
                    return "n/a";
                }
            }
        }

        public string ProductCompany
        {
            get
            {
                try
                {
                    return _assemblyInfoService.Company;
                }
                catch
                {
                    return "n/a";
                }
            }
        }

        public string OperatingSystemInfo
        {
            get
            {
                try
                {
                    var result = new StringBuilder();
                    using (var mos = new ManagementObjectSearcher("select * from Win32_OperatingSystem"))
                    {
                        foreach (var managementObject in mos.Get())
                        {
                            if (managementObject["Caption"] != null)
                            {
                                result.Append("Operating System Name:  " + managementObject["Caption"]).Append(Environment.NewLine);
                            }
                            if (managementObject["OSArchitecture"] != null)
                            {
                                result.Append("Operating System Architecture: " + managementObject["OSArchitecture"]).Append(Environment.NewLine);
                            }
                            if (managementObject["CSDVersion"] != null)
                            {
                                result.Append("Operating System Service Pack:  " + managementObject["CSDVersion"]).Append(Environment.NewLine);
                            }
                            if (managementObject["BuildNumber"] != null)
                            {
                                result.Append("Build Version:  " + managementObject["BuildNumber"]).Append(Environment.NewLine);
                            }
                            if (managementObject["SerialNumber"] != null)
                            {
                                result.Append("Serial Number:  " + managementObject["SerialNumber"]).Append(Environment.NewLine);
                            }
                            if (managementObject["Version"] != null)
                            {
                                result.Append("Version:  " + managementObject["Version"]).Append(Environment.NewLine);
                            }
                        }
                    }

                    return result.ToString();
                }
                catch
                {
                    return "n/a";
                }
            }
        }
    }
}