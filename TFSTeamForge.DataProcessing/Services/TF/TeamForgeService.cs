using CollabNet.TeamForge.Library;
using CollabNet.TeamForge.Library.Model;
using CollabNet.TeamForge.Library.Model.MainSvc;
using CollabNet.TeamForge.Library.Model.TrackerSvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFSTeamForge.DataProcessing.Models.TeamSystem;
using TFSTeamForge.DataProcessing.Models.TeamSystem.TeamForge;

namespace TFSTeamForge.DataProcessing.Services.TF
{
    public class TeamForgeService : ITeamForgeService
    {
        private IContext _tfContext;
        private ICollabNetSvc _clientService;
        private ITrackerSvc _trackerService;
        private IProjectRow _currentProject;
        private string _sessionKey;

        private TeamForgeConnection Connection { get; set; }

        public void SetConnection(TeamForgeConnection connection)
        {
            Connection = connection;
            EnsureConnection();
        }

        public IWorkItem CreateWorkItem(string workItemType, string title, string description)
        {
            EnsureConnected();
            if (TrackerService == null)
            {
                return null;
            }
            IWorkItem newItem = null;
            var tracker = TrackerService.GetTrackerList(_currentProject.Id).Where(tr => string.Compare(tr.Title, workItemType, true) == 0).FirstOrDefault();
            if (tracker == null)
            {
                return newItem;
            }
            var artifact = TrackerService.CreateArtifact(tracker.Id, title, description, Group: "", Category: "", Status: "Open", Customer: "", Priority: 4, EstimatedHours: 0, AssignedTo: "", ReleaseID: "", FlexFields: null, FlexFieldTypes: null, AttachmentFileName: "", AttachmentMimeType: "", AttachmentFileId: "");
            if (artifact != null)
            {
                artifact.FlexFields["Project"] = Connection.ProjectName;
                IAttachmentDO attachment = null;
                TrackerService.SetArtifactData(artifact, "", attachment);
                newItem = new TeamForgeArtifact()
                {
                    Id = artifact.ID,
                    Title = artifact.Title,
                    Description = artifact.Description
                };
            }
            return newItem;
        }

        public IWorkItem GetWorkItem(string id)
        {
            EnsureConnected();
            if (TrackerService == null)
            {
                return null;
            }
            TeamForgeArtifact tfTracker = null;
            var filters = TrackerService.CreateArtifactFilterCollection();

            var trackerList = TrackerService.GetTrackerList(_currentProject.Id);

            var artifact = TrackerService.GetArtifactList(_currentProject.Id, filters).Where(a => string.Compare(a.Id, id, true) == 0).FirstOrDefault();
            if (artifact != null)
            {
                tfTracker = new TeamForgeArtifact()
                {
                    Id = artifact.Id,
                    Title = artifact.Title,
                    Description = artifact.Description
                };
            }
            return tfTracker;
        }

        public void Dispose()
        {
            Disconnect();
        }

        private bool IsConnected { get; set; }

        private bool Connect()
        {
            EnsureConnection();
            IsConnected = false;
            try
            {
                _tfContext = new Context();
                _tfContext.ContextName = "TfsTfConnection";
                _tfContext.ServerHost = Connection.ServerHost;
                _tfContext.UserName = Connection.UserName;
                _tfContext.Password = Connection.Password;
                _tfContext.UseSSL = Connection.UseHttps;

                _clientService = TeamForgeServiceFactory.CreateServiceClient<ICollabNetSvc>(_tfContext);

                var clientIdentifier = "";
                _clientService.Login(clientIdentifier);
                _sessionKey = _tfContext.SessionKey;

                _currentProject = _clientService.GetProjectListForUser(Connection.UserName).Where(
                    p => string.Compare(p.Title, Connection.ProjectName.Trim(), true) == 0).FirstOrDefault();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }

            IsConnected = _currentProject != null;

            return IsConnected;
        }

        private void Disconnect()
        {
            if (_trackerService != null)
            {
                _trackerService.Dispose();
                _trackerService = null;
            }
            if (_clientService != null)
            {
                _clientService.Dispose();
                _clientService = null;
            }
            if (_tfContext != null)
            {
                _tfContext = null;
            }
        }

        private ITrackerSvc TrackerService
        {
            get
            {
                if (_trackerService == null)
                {
                    if (IsConnected)
                    {
                        _trackerService = TeamForgeServiceFactory.CreateServiceClient<ITrackerSvc>(_tfContext);
                    }
                }
                return _trackerService;
            }
        }

        private void EnsureConnected()
        {
            if (!IsConnected)
            {
                Connect();
                if (!IsConnected)
                {
                    throw new InvalidOperationException("Unable to connect to TeamForge");
                }
            }   
        }

        private void EnsureConnection()
        {
            if (Connection == null)
            {
                throw new ArgumentException("Connection cannot be empty");
            }
            if (string.IsNullOrWhiteSpace(Connection.ProjectName))
            {
                throw new ArgumentNullException("Connection.ProjectName");
            }
            if (string.IsNullOrWhiteSpace(Connection.ServerHost))
            {
                throw new ArgumentNullException("Connection.ServerUrl");
            }
            if (string.IsNullOrWhiteSpace(Connection.UserName))
            {
                throw new ArgumentNullException("Connection.UserName");
            }
            if (string.IsNullOrWhiteSpace(Connection.Password))
            {
                throw new ArgumentNullException("Connection.Password");
            }
        }
    }
}
