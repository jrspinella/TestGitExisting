using System.ServiceModel;
using System.ServiceProcess;
using TFSMessageQueueProcess;

namespace TFSTeamForgeSyncService
{
    public partial class TFSTeamForgeService : ServiceBase
    {
        internal static ServiceHost TFSServiceHost = null;

        public TFSTeamForgeService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            QueueLogger.Initialize();
            if (TFSServiceHost != null)
            {
                TFSServiceHost.Close();
            }
            TFSServiceHost = new ServiceHost(typeof(TFSMessageService));
            TFSServiceHost.Open();

        }

        protected override void OnStop()
        {
            if (TFSServiceHost != null)
            {
                TFSServiceHost.Close();
                TFSServiceHost = null;
            }

        }
    }
}
