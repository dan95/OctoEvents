using Newtonsoft.Json;

namespace OctoEvents.Domain.ViewModel
{
    public class EventViewModel
    {
        [JsonProperty("action")]
        public string Action { get; set; } = string.Empty;
        [JsonProperty("issue")]
        public IssueViewModel? Issue { get; set; }
        [JsonProperty("repository")]
        public RepositoryViewModel? Repository { get; set; }
        [JsonProperty("sender")]
        public UserViewModel? Sender { get; set; }
    }
}
