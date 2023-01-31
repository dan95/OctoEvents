using FluentValidation.Results;
using OctoEvents.Domain.Enum;
using OctoEvents.Domain.Extensions;

namespace OctoEvents.Domain.Entities
{
    public class User : BaseExternalEntity
    {
        public string Login { get; set; } = string.Empty;
        public string AvatarUrl { get; set; } = string.Empty;
        public string GravatarId { get; set; } = string.Empty;
        public string FollowersUrl { get; set; } = string.Empty;
        public string FollowingUrl { get; set; } = string.Empty;
        public string GistsUrl { get; set; } = string.Empty;
        public string StarredUrl { get; set; } = string.Empty;
        public string SubscriptionsUrl { get; set; } = string.Empty;
        public string OrganizationsUrl { get; set; } = string.Empty;
        public string RepositoriesUrl { get; set; } = string.Empty;
        public string EventsUrl { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public bool SiteAdmin { get; set; }
        public List<Repository> Repositories { get; set; } = new();
        public List<IssueEvent> Events { get; set; } = new();
        public List<Issue> Issues { get; set; } = new();

        public ValidationResult UpdateValues(User updateUser, EOperationPerformer operationPerformer = EOperationPerformer.OCTO_EVENTS_API)
        {
            var validation = base.UpdateValues(updateUser, operationPerformer);

            if (!validation.IsValid)
            {
                return validation;
            }

            Login = updateUser.Login;
            Type = updateUser.Type;
            AvatarUrl = updateUser.AvatarUrl;
            GravatarId = updateUser.GravatarId;
            FollowersUrl = updateUser.FollowersUrl;
            FollowingUrl = updateUser.FollowingUrl;
            GistsUrl = updateUser.GistsUrl;
            StarredUrl = updateUser.StarredUrl;
            SubscriptionsUrl = updateUser.SubscriptionsUrl;
            OrganizationsUrl = updateUser.OrganizationsUrl;
            RepositoriesUrl = updateUser.RepositoriesUrl;
            EventsUrl = updateUser.EventsUrl;

            return validation;
        }
    }
}