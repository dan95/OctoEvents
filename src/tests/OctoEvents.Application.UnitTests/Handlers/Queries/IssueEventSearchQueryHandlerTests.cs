using AutoMapper;
using Castle.Core.Logging;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using OctoEvents.Application.Handlers.Queries;
using OctoEvents.CrossCutting.Interfaces.Repositories;
using OctoEvents.Domain.Entities;
using OctoEvents.Tests.Configuration;
using OctoEvents.Tests.Configuration.Fixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoEvents.Application.UnitTests.Handlers.Queries
{
    public class IssueEventSearchQueryHandlerTests : BaseTestFixture, IClassFixture<DependencyInjectionFixture>
    {
        private static readonly Action<IServiceCollection> _action = (services) =>
        {
            services.AddScoped(x => new Mock<IUnitOfWork>());
            services.AddScoped(x => new Mock<IIssueRepository>());
            services.AddScoped(x => new Mock<IRepositoriesRepository>());
            services.AddScoped(x => new Mock<IUserRepository>());


            services.AddScoped(x => x.GetRequiredService<Mock<IRepositoriesRepository>>().Object);
            services.AddScoped(x => x.GetRequiredService<Mock<IUserRepository>>().Object);
            services.AddScoped(x => x.GetRequiredService<Mock<IUnitOfWork>>().Object);
            services.AddScoped(x => x.GetRequiredService<Mock<IIssueRepository>>().Object);
        };

        private readonly IssueEventSearchQueryHandler _handler;

        public IssueEventSearchQueryHandlerTests(DependencyInjectionFixture fixture) : base(fixture, _action)
        {
            _handler = new(
                _provider.GetRequiredService<IIssueRepository>(),
                _provider.GetRequiredService<IMapper>(),
                _provider.GetRequiredService<ILogger<IssueEventSearchQueryHandler>>()
            );
        }

        [Fact(DisplayName = "It should return an empty list in case the given issue is not found.")]
        public async Task ItShouldReturnAnEmptyListIfNoIssueIsFound()
        {
            var result = await _handler.Handle(new(322), _cancellationTokenSource.Token);

            result.Should().BeEmpty();
        }

        [Fact(DisplayName = "It should return an empty list in case the given issue is not found.")]
        public async Task ItShouldReturnIssueEventList()
        {
            var issue = GetIssue();
            _provider.GetRequiredService<Mock<IIssueRepository>>()
                .Setup(x => x.GetCompleteByExternalIdAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(issue));

            var result = await _handler.Handle(new(322), _cancellationTokenSource.Token);

            result.Should().HaveCount(2);

            var orderedEvents = issue!.Events.OrderByDescending(x => x.CreatedAt).ToList();

            for (int i = 0; i < orderedEvents.Count; i++)
            {
                result[i].Action.Should().Be(orderedEvents[i].Action);
                result[i].CreatedAt.Should().Be(issue!.ExternalCreationDate);
                result[i].CreatedBy.Should().Be(issue!.User.Login);
                result[i].UpdatedAt.Should().Be(orderedEvents[i].CreatedAt);
                result[i].UpdatedBy.Should().Be(orderedEvents[i].Sender.Login);
                result[i].NodeId.Should().Be(issue!.NodeId);
            }
        }

        private Issue? GetIssue()
        {
            var issue = new Issue
            {
                ExternalId = 322,
                ExternalCreationDate = new(2023, 01, 29),
                NodeId = "H)(d8SNHD-*s)nDH*SN",
                User = new()
                {
                    Login = "ISSUE_CREATOR_LOGIN",
                    ExternalId = 3452351
                },
                Events = new()
                {
                    new()
                    {
                        Action = "opened",
                        CreatedAt = new(2023, 01, 29),
                        Sender = new()
                        {
                            Login = "ISSUE_CREATOR_LOGIN",
                            ExternalId = 3452351
                        }
                    },
                    new()
                    {
                        Action = "closed",
                        CreatedAt = new(2023, 01, 30),
                        Sender = new()
                        {
                            Login = "ISSUE_CONTRIBUTOR_LOGIN",
                            ExternalId = 4123213
                        }
                    }
                }
            };

            foreach (var item in issue.Events)
            {
                item.Issue = issue;
                item.Issue = issue;
            }

            return issue;
        }
    }
}
