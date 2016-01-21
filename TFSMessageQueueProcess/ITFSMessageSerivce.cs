using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace TFSMessageQueueProcess
{
    [ServiceContract]
    public interface ITFSMessageService
    {
        [OperationContract(IsOneWay = true)]
        void NewWorkItemMessage(string messageData);

        [OperationContract(IsOneWay = true)]
        void NewGitPushMessage(string messageData);
    }
}
