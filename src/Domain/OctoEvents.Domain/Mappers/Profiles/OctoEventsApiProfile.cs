using AutoMapper;
using OctoEvents.Domain.Entities;
using OctoEvents.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoEvents.Domain.Mappers.Profiles
{
    public class OctoEventsApiProfile : Profile
    {
        public OctoEventsApiProfile()
        {
            CreateMap<EventViewModel, IssueEvent>()
                .ForMember(x => x.Action, x => x.MapFrom(c => c.Action))
                .ForMember(x => x.Issue, x => x.Ignore())
                .ForMember(x => x.Sender, x => x.MapFrom(c => c.Sender));

            CreateMap<EventViewModel, Issue>()
                .ForMember(x => x.ExternalId, x => x.MapFrom(c => c.Issue!.ExternalId))
                .ForMember(x => x.NodeId, x => x.MapFrom(c => c.Issue!.NodeId))
                .ForMember(x => x.Number, x => x.MapFrom(c => c.Issue!.Number))
                .ForMember(x => x.Title, x => x.MapFrom(c => c.Issue!.Title))
                .ForMember(x => x.Body, x => x.MapFrom(c => c.Issue!.Body))
                .ForMember(x => x.ExternalCreationDate, x => x.MapFrom(c => c.Issue!.CreatedAt))
                .ForMember(x => x.HtmlUrl, x => x.MapFrom(c => c.Issue!.HtmlUrl))
                .ForMember(x => x.CommentCount, x => x.MapFrom(c => c.Issue!.CommentCount))
                .ForMember(x => x.CommentsUrl, x => x.MapFrom(c => c.Issue!.CommentsUrl))
                .ForMember(x => x.Locked, x => x.MapFrom(c => c.Issue!.Locked))
                .ForMember(x => x.LabelsUrl, x => x.MapFrom(c => c.Issue!.LabelsUrl))
                .ForMember(x => x.RepositoryUrl, x => x.MapFrom(c => c.Issue!.RepositoryUrl))
                .ForMember(x => x.State, x => x.MapFrom(c => c.Issue!.State))
                .ForMember(x => x.StateReason, x => x.MapFrom(c => c.Issue!.StateReason))
                .ForMember(x => x.TimelineUrl, x => x.MapFrom(c => c.Issue!.TimelineUrl))
                .ForMember(x => x.ExternalLastUpdate, x => x.MapFrom(c => c.Issue!.UpdatedAt))
                .ForMember(x => x.Url, x => x.MapFrom(c => c.Issue!.Url))
                .ForMember(x => x.User, x => x.MapFrom(c => c.Issue!.User))
                .ForMember(x => x.Repository, x => x.MapFrom(c => c.Repository))
                .ForMember(x => x.ClosedAt, x => x.MapFrom(c => c.Issue!.ClosedAt))
                .AfterMap((x, o, c) =>
                {
                    o.Events = new List<IssueEvent> { c.Mapper.Map<IssueEvent>(x) };
                });

            CreateMap<RepositoryViewModel, Repository>()
                .ForMember(x => x.Private, x => x.MapFrom(c => c.Private))
                .ForMember(x => x.IssuesUrl, x => x.MapFrom(c => c.IssuesUrl))
                .ForMember(x => x.SubscribersUrl, x => x.MapFrom(c => c.SubscribersUrl))
                .ForMember(x => x.Archived, x => x.MapFrom(c => c.Archived))
                .ForMember(x => x.CollaboratorsUrl, x => x.MapFrom(c => c.CollaboratorsUrl))
                .ForMember(x => x.ExternalCreationDate, x => x.MapFrom(c => c.CreatedAt))
                .ForMember(x => x.Description, x => x.MapFrom(c => c.Description))
                .ForMember(x => x.Disabled, x => x.MapFrom(c => c.Disabled))
                .ForMember(x => x.DownloadsUrl, x => x.MapFrom(c => c.DownloadsUrl))
                .ForMember(x => x.EventsUrl, x => x.MapFrom(c => c.EventsUrl))
                .ForMember(x => x.ExternalId, x => x.MapFrom(c => c.ExternalId))
                .ForMember(x => x.Fork, x => x.MapFrom(c => c.Fork))
                .ForMember(x => x.ForkCount, x => x.MapFrom(c => c.ForkCount))
                .ForMember(x => x.ForksUrl, x => x.MapFrom(c => c.ForksUrl))
                .ForMember(x => x.FullName, x => x.MapFrom(c => c.FullName))
                .ForMember(x => x.GitTagsUrl, x => x.MapFrom(c => c.GitTagsUrl))
                .ForMember(x => x.HtmlUrl, x => x.MapFrom(c => c.HtmlUrl))
                .ForMember(x => x.Name, x => x.MapFrom(c => c.Name))
                .ForMember(x => x.Owner, x => x.MapFrom(c => c.Owner))
                .ForMember(x => x.SubscribersUrl, x => x.MapFrom(c => c.SubscribersUrl))
                .ForMember(x => x.SubscriptionUrl, x => x.MapFrom(c => c.SubscriptionUrl))
                .ForMember(x => x.TagsUrl, x => x.MapFrom(c => c.TagsUrl))
                .ForMember(x => x.ExternalLastUpdate, x => x.MapFrom(c => c.UpdatedAt))
                .ForMember(x => x.Url, x => x.MapFrom(c => c.Url));

            CreateMap<UserViewModel, User>()
                .ForMember(x => x.AvatarUrl, x => x.MapFrom(c => c.AvatarUrl))
                .ForMember(x => x.ExternalCreationDate, x => x.MapFrom(c => c.CreatedAt))
                .ForMember(x => x.EventsUrl, x => x.MapFrom(c => c.EventsUrl))
                .ForMember(x => x.ExternalId, x => x.MapFrom(c => c.ExternalId))
                .ForMember(x => x.FollowersUrl, x => x.MapFrom(c => c.FollowersUrl))
                .ForMember(x => x.FollowingUrl, x => x.MapFrom(c => c.FollowingUrl))
                .ForMember(x => x.GistsUrl, x => x.MapFrom(c => c.GistsUrl))
                .ForMember(x => x.GravatarId, x => x.MapFrom(c => c.GravatarId))
                .ForMember(x => x.Login, x => x.MapFrom(c => c.Login))
                .ForMember(x => x.NodeId, x => x.MapFrom(c => c.NodeId))
                .ForMember(x => x.OrganizationsUrl, x => x.MapFrom(c => c.OrganizationsUrl))
                .ForMember(x => x.RepositoriesUrl, x => x.MapFrom(c => c.RepositoriesUrl))
                .ForMember(x => x.SiteAdmin, x => x.MapFrom(c => c.SiteAdmin))
                .ForMember(x => x.StarredUrl, x => x.MapFrom(c => c.StarredUrl))
                .ForMember(x => x.SubscriptionsUrl, x => x.MapFrom(c => c.SubscriptionsUrl))
                .ForMember(x => x.Type, x => x.MapFrom(c => c.Type))
                .ForMember(x => x.ExternalLastUpdate, x => x.MapFrom(c => c.UpdatedAt))
                .ForMember(x => x.Url, x => x.MapFrom(c => c.Url));

            CreateMap<IssueEvent, IssueEventItemViewModel>()
                .ForMember(x => x.CreatedBy, x => x.MapFrom(c => c.Issue.User.Login))
                .ForMember(x => x.CreatedAt, x => x.MapFrom(c => c.Issue.ExternalCreationDate))
                .ForMember(x => x.UpdatedAt, x => x.MapFrom(c => c.CreatedAt))
                .ForMember(x => x.Action, x => x.MapFrom(c => c.Action))
                .ForMember(x => x.ExternalId, x => x.MapFrom(c => c.Issue.ExternalId))
                .ForMember(x => x.NodeId, x => x.MapFrom(c => c.Issue.NodeId))
                .ForMember(x => x.UpdatedBy, x => x.MapFrom(c => c.Sender.Login));
        }
    }
}
