using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TFSTeamForge.DataProcessing.Models.TeamSystem.TeamFoundation
{
    public class TeamFoundationConnection
    {
        public string Collection { get; set; }
        public string ProjectName { get; set; }
        public string ServerUrl { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
