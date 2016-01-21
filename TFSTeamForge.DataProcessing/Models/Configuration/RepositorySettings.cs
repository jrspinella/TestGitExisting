using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TFSTeamForge.DataProcessing.Models.Configuration
{
    public class RepositorySettings
    {
        [JsonProperty(PropertyName = "source")]
        public RepositoryConfiguration Source { get; set; }
        [JsonProperty(PropertyName = "destination")]
        public RepositoryConfiguration Destination { get; set; }
        [JsonProperty(PropertyName = "defaultRemoteName")]
        public string DefaultRemoteName { get; set; }
    }
}