using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFSTeamForge.DataProcessing.Models.Configuration
{
    public abstract class MappableType
    {
        [JsonProperty(PropertyName = "mappingId")]
        public int Id { get; set; }
    }
}
