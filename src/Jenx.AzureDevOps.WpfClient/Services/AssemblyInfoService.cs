using System;
using System.Reflection;

namespace Jenx.AzureDevOps.WpfClient.Services
{
    public class AssemblyInfoService : IAssemblyInfoService
    {
        private readonly Assembly _assembly;

        public AssemblyInfoService(Assembly assembly)
        {
            _assembly = assembly;
        }

        public string Company
        {
            get { return GetExecutingAssemblyAttribute<AssemblyCompanyAttribute>(a => a.Company); }
        }

        public string Product
        {
            get { return GetExecutingAssemblyAttribute<AssemblyProductAttribute>(a => a.Product); }
        }

        public string Copyright
        {
            get { return GetExecutingAssemblyAttribute<AssemblyCopyrightAttribute>(a => a.Copyright); }
        }

        public string Trademark
        {
            get { return GetExecutingAssemblyAttribute<AssemblyTrademarkAttribute>(a => a.Trademark); }
        }

        public string Title
        {
            get { return GetExecutingAssemblyAttribute<AssemblyTitleAttribute>(a => a.Title); }
        }

        public string Description
        {
            get { return GetExecutingAssemblyAttribute<AssemblyDescriptionAttribute>(a => a.Description); }
        }

        public string Configuration
        {
            get { return GetExecutingAssemblyAttribute<AssemblyDescriptionAttribute>(a => a.Description); }
        }

        public string FileVersion
        {
            get { return GetExecutingAssemblyAttribute<AssemblyFileVersionAttribute>(a => a.Version); }
        }

        public Version Version => _assembly.GetName().Version;

        public string VersionFull => Version.ToString();

        public string VersionMajor => Version.Major.ToString();

        public string VersionMinor => Version.Minor.ToString();

        public string VersionBuild => Version.Build.ToString();

        public string VersionRevision => Version.Revision.ToString();

        private string GetExecutingAssemblyAttribute<T>(Func<T, string> value) where T : Attribute
        {
            T attribute = (T)Attribute.GetCustomAttribute(_assembly, typeof(T));
            return value.Invoke(attribute);
        }
    }
}