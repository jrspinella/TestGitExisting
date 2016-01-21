using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TFSTeamForge.DataProcessing.Models.ServiceHook
{
    public class Message
    {
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }
        [JsonProperty(PropertyName = "html")]
        public string HTML { get; set; }
        [JsonProperty(PropertyName = "markdown")]
        public string Markdown { get; set; }
    }
}