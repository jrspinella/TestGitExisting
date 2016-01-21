using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TFSTeamForge.DataProcessing.Models.Configuration
{
    public class ServiceHookSettings
    {
        public ServiceHookSettings()
        {
            TFS = new TFSSettings();
            TeamForge = new TeamForgeSettings();
        }

        [JsonProperty(PropertyName = "tfs")]
        public TFSSettings TFS { get; set; }
        
        [JsonProperty(PropertyName = "tf")]
        public TeamForgeSettings TeamForge { get; set; }

        [JsonProperty(PropertyName = "repositories")]
        public List<RepositorySettings> Repositories { get; set; }
    }
}
