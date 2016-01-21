using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFSTeamForge.DataProcessing.Models.Configuration;

namespace TFSTeamForge.DataProcessing.Services
{
    public interface IConfigurationProviderService
    {
        Task<ServiceHookSettings> GetServiceHookFileSettingsAsync(string path);
        ServiceHookSettings GetServiceHookFileSettings(string path);
    }
}
