using Newtonsoft.Json;
using System.IO;

namespace Jenx.AzureDevOps.WpfClient.Settings
{
    public class AppSettingsService : ISettings
    {
        private readonly string _configFilePath;

        public AppSettingsService(string configFilePath)
        {
            _configFilePath = configFilePath;
        }

        public string PersonalAccessToken { get; set; }

        public string OrganizationName { get; set; }

        public void Store()
        {
            try
            {
                var configFolder = Path.GetDirectoryName(_configFilePath);

                if (!Directory.Exists(configFolder))
                    Directory.CreateDirectory(configFolder);

                File.WriteAllText(_configFilePath, JsonConvert.SerializeObject(this));
            }
            catch
            {
                //log and rethrow -> app will catch error
            }
        }

        public void Load()
        {
            try
            {
                var data = JsonConvert.DeserializeObject<AppSettingsService>(File.ReadAllText(_configFilePath));

                OrganizationName = data.OrganizationName;
                PersonalAccessToken = data.PersonalAccessToken;
            }
            catch
            {
                //log and rethrow -> app will catch error
            }
        }
    }
}