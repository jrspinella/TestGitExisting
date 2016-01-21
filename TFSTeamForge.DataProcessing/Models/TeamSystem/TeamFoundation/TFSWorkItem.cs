using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TFSTeamForge.DataProcessing.Models.TeamSystem.TeamFoundation
{
    public class TFSWorkItemCollection
    {
        public TFSWorkItemCollection()
        {
            WorkItems = new List<TFSWorkItem>();
        }

        [JsonProperty(PropertyName = "count")]
        public int Count { get; set; }
        [JsonProperty(PropertyName = "value")]
        public List<TFSWorkItem> WorkItems { get; set; }
    }

    public class TFSWorkItem
    {
        public TFSWorkItem()
        {
            Fields = new Dictionary<string, object>();
        }

        [JsonProperty(PropertyName = "id")]
        public int? Id { get; set; }
        [JsonProperty(PropertyName = "rev")]
        public int? Revision { get; set; }
        [JsonProperty(PropertyName = "fields")]
        public IDictionary<string, object> Fields { get; set; }

        public string Title
        {
            get
            {
                return Fields.RetrieveFieldValue("System.Title");
            }
        }

        public string Description
        {
            get
            {
                return Fields.RetrieveFieldValue("System.Description");
            }
        }
    }
}
