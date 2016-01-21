using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFSTeamForge.DataProcessing.Models.TeamSystem;
using TFSTeamForge.DataProcessing.Models.TeamSystem.TeamForge;
using TFSTeamForge.DataProcessing.Models.TeamSystem.TeamFoundation;

namespace TFSTeamForge.DataProcessing.Services.TF
{
    public interface ITeamForgeService : IDisposable
    {
        void SetConnection(TeamForgeConnection connection);
        IWorkItem CreateWorkItem(string workItemType, string title, string description);
        IWorkItem GetWorkItem(string id);
    }
}
