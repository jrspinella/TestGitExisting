using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TFSTeamForge.DataProcessing.Models.ServiceHook
{
    public class CodeRepository
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
        [JsonProperty(PropertyName = "defaultBranch")]
        public string DefaultBranch { get; set; }
        [JsonProperty(PropertyName = "remoteUrl")]
        public string RemoteUrl { get; set; }
        [JsonProperty(PropertyName = "project")]
        public CodeProject Project { get; set; }
    }
}