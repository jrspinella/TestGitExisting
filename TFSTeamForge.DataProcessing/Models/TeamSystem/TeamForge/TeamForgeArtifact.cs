using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TFSTeamForge.DataProcessing.Models.TeamSystem.TeamForge
{
    public class TeamForgeArtifact : IWorkItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Id { get; set; }
    }
}