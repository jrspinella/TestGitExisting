using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TFSTeamForge.DataProcessing.Models.TeamSystem.TeamFoundation
{
    public class WorkItemUpdate
    {
        [JsonProperty("op")]
        public string Operation { get; set; }
        [JsonProperty("path")]
        public string Path { get; set; }
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
