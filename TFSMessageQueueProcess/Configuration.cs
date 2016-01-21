using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFSTeamForge.DataProcessing.Services;

namespace TFSMessageQueueProcess
{
    internal class Configuration
    {
        private static IConfigurationProviderService _provider;

        public static IConfigurationProviderService Provider
        {
            get
            {
                if (_provider == null)
                {
                    _provider = new ConfigurationProviderService();
                }
                return _provider;
            }
        }
    }
}
