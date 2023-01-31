using FluentValidation.Results;
using OctoEvents.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoEvents.Domain.Entities
{
    public class Issue : BaseExternalEntity
    {
        public string RepositoryUrl { get; set; } = string.Empty;
        public string LabelsUrl { get; set; } = string.Empty;
        public string CommentsUrl { get; set;} = string.Empty;
        public string HtmlUrl { get; set; } = string.Empty;
        public long Number { get; set; }
        public string? Title { get; set; }
        public string State { get; set; } = string.Empty;
        public bool Locked { get; set; }
        public long CommentCount { get; set; }
        public DateTime? ClosedAt { get; set; }
        public string? Body { get; set; }
        public string TimelineUrl { get; set; } = string.Empty;
        public string? StateReason { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; } = new();
        public Guid RepositoryId { get; set; }
        public Repository Repository { get; set; } = new();
        public List<IssueEvent> Events { get; set; } = new();

        public ValidationResult UpdateValues(Issue updateIssue, EOperationPerformer operationPerformer = EOperationPerformer.OCTO_EVENTS_API)
        {
            var validation = base.UpdateValues(updateIssue, operationPerformer);

            if (!validation.IsValid)
            {
                return validation;
            }

            RepositoryUrl = updateIssue.RepositoryUrl;
            LabelsUrl = updateIssue.LabelsUrl;
            CommentsUrl = updateIssue.CommentsUrl;
            HtmlUrl = updateIssue.HtmlUrl;
            Number = updateIssue.Number;
            Title = updateIssue.Title;
            State = updateIssue.State;
            Locked = updateIssue.Locked;
            CommentCount = updateIssue.CommentCount;
            TimelineUrl = updateIssue.TimelineUrl;
            StateReason = updateIssue.StateReason;
            ClosedAt = updateIssue.ClosedAt;

            return validation;
        }
    }
}
