using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TFSTeamForge.DataProcessing.Models.ServiceHook
{
    public class CodePushedHookContent
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
        [JsonProperty(PropertyName = "resource")]
        public CodePushedResource Resource { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}