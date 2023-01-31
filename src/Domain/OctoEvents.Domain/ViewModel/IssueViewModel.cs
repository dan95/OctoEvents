using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoEvents.Domain.ViewModel
{
    public class IssueViewModel : BaseExternalObject
    {
        [JsonProperty("repository_url")]
        public string RepositoryUrl { get; set; } = string.Empty;

        [JsonProperty("labels_url")]
        public string LabelsUrl { get; set; } = string.Empty;

        [JsonProperty("comments_url")]
        public string CommentsUrl { get; set; } = string.Empty;

        [JsonProperty("number")]
        public long Number { get; set; }

        [JsonProperty("title")]
        public string? Title { get; set; }

        [JsonProperty("state")]
        public string State { get; set; } = string.Empty;

        [JsonProperty("locked")]
        public bool Locked { get; set; }

        [JsonProperty("comments")]
        public long CommentCount { get; set; }


        [JsonProperty("closed_at")]
        public DateTime? ClosedAt { get; set; }

        [JsonProperty("body")]
        public string? Body { get; set; }

        [JsonProperty("timeline_url")]
        public string? TimelineUrl { get; set; }

        [JsonProperty("state_reason")]
        public string? StateReason { get; set; }

        [JsonProperty("user")]
        public UserViewModel? User { get; set; }
    }
}
