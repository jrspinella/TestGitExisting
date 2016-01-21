using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TFSTeamForge.DataProcessing.Models.ServiceHook
{
    public class WorkItemResource
    {
        [JsonProperty("fields")]
        public JObject Fields { get; set; }
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}
