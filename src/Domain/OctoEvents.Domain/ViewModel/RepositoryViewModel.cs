using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoEvents.Domain.ViewModel
{
    public class RepositoryViewModel : BaseExternalObject
    {
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("full_name")]
        public string FullName { get; set; } = string.Empty;

        [JsonProperty("private")]
        public bool Private { get; set; }

        [JsonProperty("owner")]
        public UserViewModel? Owner { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }

        [JsonProperty("fork")]
        public bool Fork { get; set; }

        [JsonProperty("archived")]
        public bool Archived { get; set; }

        [JsonProperty("disabled")]
        public bool Disabled { get; set; }

        [JsonProperty("forks_count")]
        public long ForkCount { get; set; }

        [JsonProperty("forks_url")]
        public string? ForksUrl { get; set; }

        [JsonProperty("collaborators_url")]
        public string? CollaboratorsUrl { get; set; }

        [JsonProperty("tags_url")]
        public string? TagsUrl { get; set; }

        [JsonProperty("git_tags_url")]
        public string? GitTagsUrl { get; set; }

        [JsonProperty("contributors_url")]
        public string? ContributorsUrl { get; set; }

        [JsonProperty("subscribers_url")]
        public string? SubscribersUrl { get; set; }

        [JsonProperty("subscription_url")]
        public string? SubscriptionUrl { get; set; }

        [JsonProperty("commits_url")]
        public string? CommitsUrl { get; set; }

        [JsonProperty("downloads_url")]
        public string? DownloadsUrl { get; set; }

        [JsonProperty("issues_url")]
        public string? IssuesUrl { get; set; }
    }
}
