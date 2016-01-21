using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TFSTeamForge.DataProcessing.Models.Configuration;

namespace TFSTeamForge.DataProcessing.Services
{
    public class ConfigurationProviderService : IConfigurationProviderService
    {
        public Task<ServiceHookSettings> GetServiceHookFileSettingsAsync(string path)
        {
            return SettingsLoader.LoadFromConfigFileAsync(path);
        }

        public ServiceHookSettings GetServiceHookFileSettings(string path)
        {
            return SettingsLoader.LoadFromConfigFile(path);
        }
    }
}