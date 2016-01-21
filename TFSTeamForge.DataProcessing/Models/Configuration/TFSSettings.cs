using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFSTeamForge.DataProcessing.Models.Configuration
{
    public class TFSSettings
    {
        public TFSSettings()
        {
            Connection = new ServerConnection();
            //ValidCollections = new List<string>();
            ValidProjects = new List<ProjectMapping>();
            ValidWorkItems = new List<WorkItemMapping>();
        }

        [JsonProperty(PropertyName = "connection")]
        public ServerConnection Connection { get; set; }
        
        //[JsonProperty(PropertyName = "validCollections")]
        //public List<string> ValidCollections { get; set; }
        
        [JsonProperty(PropertyName = "validProjects")]
        public List<ProjectMapping> ValidProjects { get; set; }

        [JsonProperty(PropertyName = "validWorkItemTypes")]
        public List<WorkItemMapping> ValidWorkItems { get; set; }

        [JsonProperty(PropertyName = "supportedEvents")]
        public List<string> SupportedEvents { get; set; }
    }
}
