using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFSTeamForge.DataProcessing.Models.Configuration
{
    public class ProjectMapping : MappableType
    {
        [JsonProperty(PropertyName =  "collectionName")]
        public string CollectionName { get; set; }
        [JsonProperty(PropertyName = "projectName")]
        public string ProjectName { get; set; }
        [JsonProperty(PropertyName = "areaName")]
        public string AreaName { get; set; }
    }
}
