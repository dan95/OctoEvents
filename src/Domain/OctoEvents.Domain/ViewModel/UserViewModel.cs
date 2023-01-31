using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoEvents.Domain.ViewModel
{
    public class UserViewModel : BaseExternalObject
    {
        [JsonProperty("login")]
        public string Login { get; set; } = string.Empty;

        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; } = string.Empty;

        [JsonProperty("gravatar_id")]
        public string GravatarId { get; set; } = string.Empty;

        [JsonProperty("followers_url")]
        public string FollowersUrl { get; set; } = string.Empty;

        [JsonProperty("following_url")]
        public string FollowingUrl { get; set; } = string.Empty;

        [JsonProperty("gists_url")]
        public string GistsUrl { get; set; } = string.Empty;

        [JsonProperty("starred_url")]
        public string StarredUrl { get; set; } = string.Empty;

        [JsonProperty("subscriptions_urls")]
        public string SubscriptionsUrl { get; set; } = string.Empty;

        [JsonProperty("organizations_url")]
        public string OrganizationsUrl { get; set; } = string.Empty;

        [JsonProperty("repos_url")]
        public string RepositoriesUrl { get; set; } = string.Empty;

        [JsonProperty("type")]
        public string Type { get; set; } = string.Empty;

        [JsonProperty("site_admin")]
        public bool SiteAdmin { get; set; }
    }
}
