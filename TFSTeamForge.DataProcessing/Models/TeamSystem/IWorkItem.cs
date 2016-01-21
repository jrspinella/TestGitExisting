using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TFSTeamForge.DataProcessing.Models.TeamSystem
{
    public interface IWorkItem
    {
        string Title { get; set; }
        string Description { get; set; }
        string Id { get; set; }
    }
}
