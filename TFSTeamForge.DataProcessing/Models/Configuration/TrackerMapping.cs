using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFSTeamForge.DataProcessing.Models.Configuration
{
    public class TrackerMapping : MappableType
    {
        [JsonProperty(PropertyName = "trackerName")]
        public string TrackerName { get; set; }
    }
}
