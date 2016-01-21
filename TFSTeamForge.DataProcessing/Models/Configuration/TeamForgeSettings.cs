using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFSTeamForge.DataProcessing.Models.Configuration
{
    public class TeamForgeSettings
    {
        public TeamForgeSettings()
        {
            Connection = new ServerConnection();
            Projects = new List<ProjectMapping>();
            Trackers = new List<TrackerMapping>();
        }

        [JsonProperty(PropertyName = "connection")]
        public ServerConnection Connection { get; set; }

        [JsonProperty(PropertyName = "projects")]
        public List<ProjectMapping> Projects { get; set; }
        
        [JsonProperty(PropertyName = "trackers")]
        public List<TrackerMapping> Trackers { get; set; }
    }
}
