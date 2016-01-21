using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFSTeamForge.DataProcessing.Models.TeamSystem;
using TFSTeamForge.DataProcessing.Models.TeamSystem.TeamFoundation;

namespace TFSTeamForge.DataProcessing.Services.TFS
{
    public interface ITeamFoundationService //: ITeamSystemService
    {
        void SetConnection(TeamFoundationConnection connection);
        Task<TFSWorkItem> GetWorkItemAsync(int id);
        Task<TFSWorkItemCollection> GetWorkItemsAsync(params int[] ids);
        Task<TeamProjectCollection> GetTeamProjectsAsync();
        Task<bool> AddTeamForgeArtifactIdAsync(int workItemId, string artifactId);
    }
}
