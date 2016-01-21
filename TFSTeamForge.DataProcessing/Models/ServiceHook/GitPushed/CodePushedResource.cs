using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TFSTeamForge.DataProcessing.Models.ServiceHook
{
    public class CodePushedResource
    {
        [JsonProperty(PropertyName = "pushId")]
        public int PushId { get; set; }
        [JsonProperty(PropertyName = "date")]
        public DateTime? Date { get; set; }
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
        [JsonProperty(PropertyName = "commits")]
        public List<Commit> Commits { get; set; }
        [JsonProperty(PropertyName = "repository")]
        public CodeRepository Repository { get; set; }
        [JsonProperty(PropertyName = "pushedBy")]
        public PushAuthor PushedBy { get; set; }
    }
}