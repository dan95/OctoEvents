using FluentValidation.Results;
using OctoEvents.Domain.Enum;
using OctoEvents.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoEvents.Domain.Entities
{
    public class Repository : BaseExternalEntity
    {
        public string Name { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public bool Private { get; set; }
        public Guid OwnerId { get; set; }
        public User Owner { get; set; }
        public string HtmlUrl { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool Fork { get; set; }
        public bool Archived { get; set; }
        public bool Disabled { get; set; }
        public long ForkCount { get; set; }
        public string? ForksUrl { get; set; }
        public string? CollaboratorsUrl { get; set; }
        public string? EventsUrl { get; set; }
        public string? TagsUrl { get; set; }
        public string? GitTagsUrl { get; set; }
        public string? ContributorsUrl { get; set; }
        public string? SubscribersUrl { get; set; }
        public string? SubscriptionUrl { get; set; }
        public string? CommitsUrl { get; set; }
        public string? DownloadsUrl { get; set; }
        public string? IssuesUrl { get; set; }
        public List<Issue> Issues { get; set; } = new();

        public ValidationResult UpdateValues(Repository updateRepository, EOperationPerformer operationPerformer = EOperationPerformer.OCTO_EVENTS_API)
        {
            var validation = base.UpdateValues(updateRepository, operationPerformer);

            if (!validation.IsValid)
            {
                return validation;
            }

            Name = updateRepository.Name;
            FullName = updateRepository.FullName;
            Private = updateRepository.Private;
            HtmlUrl = updateRepository.HtmlUrl;
            Description = updateRepository.Description;
            Fork = updateRepository.Fork;
            Archived = updateRepository.Archived;
            Disabled = updateRepository.Disabled;
            ForkCount = updateRepository.ForkCount;
            ForksUrl = updateRepository.ForksUrl;
            CollaboratorsUrl = updateRepository.CollaboratorsUrl;
            EventsUrl = updateRepository.EventsUrl;
            TagsUrl = updateRepository.TagsUrl;
            GitTagsUrl = updateRepository.GitTagsUrl;
            ContributorsUrl = updateRepository.ContributorsUrl;
            SubscribersUrl = updateRepository.SubscribersUrl;
            SubscriptionUrl = updateRepository.SubscriptionUrl;
            CommitsUrl = updateRepository.CommitsUrl;
            DownloadsUrl = updateRepository.DownloadsUrl;
            IssuesUrl = updateRepository.IssuesUrl;

            return validation;
        }
    }
}
