using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TFSTeamForge.DataProcessing.Models.ServiceHook
{
    public class Commit
    {
        [JsonProperty(PropertyName = "commitId")]
        public string CommitId { get; set; }
        [JsonProperty(PropertyName = "comment")]
        public string Comment { get; set; }
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
        [JsonProperty(PropertyName = "author")]
        public CommitAuthor Author { get; set; }
        [JsonProperty(PropertyName = "committer")]
        public CommitAuthor Committer { get; set; }
    }
}