using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoEvents.Domain.ViewModel
{
    public class BaseExternalObject
    {
        [JsonProperty("id")]
        public long ExternalId { get; set; }

        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [JsonProperty("node_id")]
        public string NodeId { get; set; } = string.Empty;

        [JsonProperty("url")]
        public string Url { get; set; } = string.Empty;

        [JsonProperty("events_url")]
        public string EventsUrl { get; set; } = string.Empty;

        [JsonProperty("html_url")]
        public string HtmlUrl { get; set;} = string.Empty;
    }
}
