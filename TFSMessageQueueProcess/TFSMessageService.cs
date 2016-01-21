using log4net;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using TFSTeamForge.DataProcessing;
using TFSTeamForge.DataProcessing.Models.ServiceHook;
using TFSTeamForge.DataProcessing.Models.TeamSystem;
using TFSTeamForge.DataProcessing.Models.TeamSystem.TeamForge;
using TFSTeamForge.DataProcessing.Models.TeamSystem.TeamFoundation;
using TFSTeamForge.DataProcessing.Services;
using TFSTeamForge.DataProcessing.Services.TF;
using TFSTeamForge.DataProcessing.Services.TFS;

namespace TFSMessageQueueProcess
{
    public class TFSMessageService : ITFSMessageService
    {
        public TFSMessageService()
        {
        }

        private string ConfigPath
        {
            get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json"); }
        }

        
        public void NewWorkItemMessage(string messageData)
        {
            WorkItemHookContent workItemEvent = null;
            try
            {
                workItemEvent = JsonConvert.DeserializeObject<WorkItemHookContent>(messageData);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
            if (workItemEvent == null)
            {
                QueueLogger.Log.Error("Invalid message data coming trying to parse new work item");
                return;
            }

            var currentSettings = Configuration.Provider.GetServiceHookFileSettings(ConfigPath);

            if (!currentSettings.TFS.SupportedEvents.Any(ev => string.Compare(ev, workItemEvent.EventType, true) == 0))
            {
                var message = string.Format("Event Id:{0} of EventType:{1} is not a supported event type.", workItemEvent.Id, workItemEvent.EventType);
                QueueLogger.Log.Info(message);
                return;
            }

            var projectName = workItemEvent.Resource.Fields.RetrieveFieldValue("System.TeamProject");
            var areaName = workItemEvent.Resource.Fields.RetrieveFieldValue("System.AreaPath");
            var type = workItemEvent.Resource.Fields.RetrieveFieldValue("System.WorkItemType");

            var projectMapping = currentSettings.TFS.ValidProjects.FirstOrDefault(pm => string.Compare(pm.ProjectName, projectName, true) == 0 && string.Compare(pm.AreaName, areaName, true) == 0);
            if (projectMapping == null)
            {
                var message = string.Format("WorkItem Id:{0} from Project/Area of {1}/{2} is not a mapped project.", workItemEvent.Resource.Id, projectName, areaName);
                QueueLogger.Log.Info(message);
                return;
            }

            var tfProjectMapping = currentSettings.TeamForge.Projects.FirstOrDefault(proj => proj.Id == projectMapping.Id);
            if (tfProjectMapping == null)
            {
                var message = string.Format("No TeamForge project mapping exists for Id:{0}", projectMapping.Id);
                QueueLogger.Log.Info(message);
                return;
            }

            var workItemMapping = currentSettings.TFS.ValidWorkItems.FirstOrDefault(wim => string.Compare(wim.WorkItemType, type, true) == 0);
            if (workItemMapping == null)
            {
                var message = string.Format("WorkItem Id:{0} of Type:{1} is not a supported work item type.", workItemEvent.Resource.Id, type);
                QueueLogger.Log.Info(message);
                return;
            }

            var trackerMapping = currentSettings.TeamForge.Trackers.FirstOrDefault(tr => tr.Id == workItemMapping.Id);
            if (trackerMapping == null)
            {
                var message = string.Format("No TeamForge tracker mapping exists for Id:{0}", workItemMapping.Id);
                QueueLogger.Log.Info(message);
                return;
            }

            var title = workItemEvent.Resource.Fields.RetrieveFieldValue("System.Title");
            var description = workItemEvent.Resource.Fields.RetrieveFieldValue("System.Description");
            if (string.IsNullOrEmpty(description))
            {
                description = "<No description>";
            }

            var tfsConnection = new TeamFoundationConnection()
            {
                Collection = projectMapping.CollectionName,
                ProjectName = projectMapping.ProjectName,
                ServerUrl = currentSettings.TFS.Connection.Server,
                UserName = currentSettings.TFS.Connection.UserName,
                Password = currentSettings.TFS.Connection.Password
            };
            var tfConnection = new TeamForgeConnection()
            {
                ServerHost = currentSettings.TeamForge.Connection.Server,
                ProjectName = tfProjectMapping.ProjectName,
                UserName = currentSettings.TeamForge.Connection.UserName,
                Password = currentSettings.TeamForge.Connection.Password,
                UseHttps = currentSettings.TeamForge.Connection.IsHttps
            };
            try
            {
                var tfsService = new TeamFoundationService();
                IWorkItem createdArtifact = null;
                using (var forgeService = new TeamForgeService())
                {
                    forgeService.SetConnection(tfConnection);
                    createdArtifact = forgeService.CreateWorkItem(trackerMapping.TrackerName, title, description);
                }
                //if (createdArtifact != null)
                //{
                //    tfsService.SetConnection(tfsConnection);
                //    var value = tfsService.AddTeamForgeArtifactIdAsync(workItemEvent.Resource.Id, createdArtifact.Id);
                //}
                QueueLogger.Log.Info(string.Format("Added artifact id: {0} to work item: {1}", "x", "x"));
            }
            catch (Exception ex)
            {
                var message = "An error occured saving the data. " + ex.ToString();
                QueueLogger.Log.Error(message);
            }
        }


        public void NewGitPushMessage(string messageData)
        {
            CodePushedHookContent codePushEvent = null;
            try
            {
                codePushEvent = JsonConvert.DeserializeObject<CodePushedHookContent>(messageData);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
            if (codePushEvent == null)
            {
                return;
            }

            var currentSettings = Configuration.Provider.GetServiceHookFileSettings(ConfigPath);

            if (!currentSettings.TFS.SupportedEvents.Any(ev => string.Compare(ev, codePushEvent.EventType, true) == 0))
            {
                var message = string.Format("Event Id:{0} of EventType:{1} is not a supported event type.", codePushEvent.Id, codePushEvent.EventType);
                QueueLogger.Log.Info(message);
                return;
            }

            var repositorySettings = currentSettings.Repositories.FirstOrDefault(rs => string.Compare(rs.Source.Name, codePushEvent.Resource.Repository.Name, true) == 0);
            if (repositorySettings == null)
            {
                var message = string.Format("Repository Name:{0} is not a valid repository target.", codePushEvent.Resource.Repository.Name);
                QueueLogger.Log.Info(message);
                return;
            }

            var gitProvider = new GitProviderService();

            gitProvider.SourceUserName = currentSettings.TFS.Connection.UserName;
            gitProvider.SourcePassword = currentSettings.TFS.Connection.Password;
            gitProvider.RemoteUserName = currentSettings.TeamForge.Connection.UserName;
            gitProvider.RemotePassword = currentSettings.TeamForge.Connection.Password;
            gitProvider.SignatureUserName = codePushEvent.Resource.PushedBy.DisplayName;
            gitProvider.SignatureUserEmail = codePushEvent.Resource.PushedBy.UniqueName.MatchEmail();
            gitProvider.DefaultRemoteName = repositorySettings.DefaultRemoteName;

            var repoPath = gitProvider.PullFromRepository(repositorySettings.Source.Url, repositorySettings.Source.Branch);
            if (string.IsNullOrEmpty(repoPath))
            {
                var message = string.Format("Failed to pull code from {0}", repositorySettings.Source.Url);
                QueueLogger.Log.Info(message);
                return;
            }

            var pushed = gitProvider.PushToRepository(repoPath, repositorySettings.Destination.Url, repositorySettings.Destination.Branch);
            if (!pushed)
            {
                var message = string.Format("Failed to push code to {0}", repositorySettings.Destination.Url);
                QueueLogger.Log.Error(message);
                return;
            }

        }
    }
}
