using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoEvents.Domain.ViewModel
{
    public class IssueEventItemViewModel
    {
        [JsonProperty("id")]
        public long ExternalId { get; set; }
        [JsonProperty("node_id")]
        public string NodeId { get; set; } = string.Empty;
        [JsonProperty("action")]
        public string Action { get; set; } = string.Empty;
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("created_by")]
        public string CreatedBy { get; set; } = string.Empty;
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
        [JsonProperty("updated_by")]
        public string UpdatedBy { get; set; } = string.Empty;
    }
}
