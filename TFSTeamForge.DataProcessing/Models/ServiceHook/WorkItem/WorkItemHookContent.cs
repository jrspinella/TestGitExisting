using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TFSTeamForge.DataProcessing.Models.ServiceHook
{
    public class WorkItemHookContent
    {
        [JsonProperty("subscriptionId")]
        public string SubscriptionId { get; set; }
        [JsonProperty("notificationId")]
        public int NotificationId { get; set; }
        [JsonProperty("eventType")]
        public string EventType { get; set; }
        [JsonProperty(PropertyName = "message")]
        public Message Message { get; set; }
        [JsonProperty(PropertyName = "detailedMessage")]
        public Message DetailedMessage { get; set; }
        [JsonProperty("resource")]
        public WorkItemResource Resource { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
