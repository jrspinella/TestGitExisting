using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TFSTeamForge.DataProcessing.Models.TeamSystem.TeamFoundation
{
    public class TeamProjectCollection
    {
        public TeamProjectCollection()
        {
            Projects = new List<TeamProject>();
        }

        [JsonProperty("count")]
        public int Count { get; set; }
        [JsonProperty("value")]
        public List<TeamProject> Projects { get; set; }
    }
}
