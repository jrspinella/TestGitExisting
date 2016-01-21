using System.ServiceModel;
using TFSMessageQueueProcess;

namespace TFSServiceTestClient
{
    internal class TFSServiceClient
    {
        internal static ServiceHost TFSServiceHost = null;

        public TFSServiceClient()
        {
        }

        public void OnStart(string[] args)
        {
            QueueLogger.Initialize();
            if (TFSServiceHost != null)
            {
                TFSServiceHost.Close();
            }
            TFSServiceHost = new ServiceHost(typeof(TFSMessageService));
            TFSServiceHost.Open();

        }

        public void OnStop()
        {
            if (TFSServiceHost != null)
            {
                TFSServiceHost.Close();
                TFSServiceHost = null;
            }

        }
    }
}
