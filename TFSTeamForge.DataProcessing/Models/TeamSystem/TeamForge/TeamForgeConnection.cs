using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TFSTeamForge.DataProcessing.Models.TeamSystem.TeamForge
{
    public class TeamForgeConnection
    {
        public string ProjectName { get; set; }
        public string ServerHost { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool UseHttps { get; set; }
    }
}
