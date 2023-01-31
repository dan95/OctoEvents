using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using OctoEvents.Domain.Entities;
using OctoEvents.Domain.ViewModel;
using OctoEvents.Tests.Configuration;
using OctoEvents.Tests.Configuration.Fixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoEvents.Domain.UnitTests.Mappers
{
    public class IssueEventItemViewModelTests : BaseTestFixture, IClassFixture<DependencyInjectionFixture>
    {
        public IssueEventItemViewModelTests(DependencyInjectionFixture fixture) : base(fixture)
        {
        }

        [Fact(DisplayName = "It should map correctly to IssueEventItemViewModel")]
        public void ItShouldMapCorrectlyToIssue()
        {
            var issueEvent = new IssueEvent();

            issueEvent.Sender = new User { ExternalId = 1231, Login = "USER_LOGIN" };
            issueEvent.Issue = new Issue
            {
                ExternalId = 123,
                NodeId = "12y3ojn12p89xy1un",
                CreatedAt = new DateTime(2023, 01, 28),
                ExternalCreationDate = new DateTime(2023, 01, 28),
                User = new User
                {
                    ExternalId = 28193,
                    Login = "USER_LOGIN_ISSUE_CREATOR"
                }
            };
            issueEvent.CreatedAt = new DateTime(2023, 01, 29);
            issueEvent.Action = "closed";

            var viewModel = _provider.GetRequiredService<IMapper>().Map<IssueEventItemViewModel>(issueEvent);

            viewModel.ExternalId.Should().Be(issueEvent.Issue.ExternalId);
            viewModel.NodeId.Should().Be(issueEvent.Issue.NodeId);
            viewModel.Action.Should().Be(issueEvent.Action);
            viewModel.CreatedAt.Should().Be(issueEvent.Issue.ExternalCreationDate);
            viewModel.UpdatedAt.Should().Be(issueEvent.CreatedAt);
            viewModel.CreatedBy.Should().Be(issueEvent.Issue.User.Login);
            viewModel.UpdatedBy.Should().Be(issueEvent.Sender.Login);
        }
    }
}
