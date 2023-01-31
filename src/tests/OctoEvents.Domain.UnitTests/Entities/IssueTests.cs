using FluentAssertions;
using OctoEvents.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoEvents.Domain.UnitTests.Entities
{
    public class IssueTests
    {
        [Fact(DisplayName = "It should not allow issue to be updated by an issue with different external ID")]
        public void ItShouldNotAllowUpdatesFromDifferentIssues()
        {
            var issue = new Issue();
            issue.ExternalId = 123;
            var differentIssue = new Issue();
            differentIssue.ExternalId = 124;

            var validation = issue.UpdateValues(differentIssue);

            validation.IsValid.Should().BeFalse();

            validation.Errors.Should().HaveCount(1);
            validation.Errors[0].PropertyName.Should().Be(nameof(Issue.ExternalId));
            validation.Errors[0].ErrorMessage.Should().Be("The external entity ID must match the updated entity's.");
        }

        [Fact(DisplayName = "It should update issue's assignable fields")]
        public void ItShouldUpdateIssue()
        {
            var issue = new Issue()
            {
                ExternalId = 123,
                ExternalCreationDate = new DateTime(2023, 1, 28),
                ClosedAt = null,
                Title = "TEST ISSUE",
                Body = "TEST BODY"
            };

            var issueUpdate = new Issue()
            {
                ExternalId = 123,
                ExternalCreationDate = new DateTime(2023, 1, 28),
                ClosedAt = new DateTime(2023, 1, 29, 20, 40, 0),
                Title = "TEST ISSUE CHANGED",
                Body = "TEST BODY CHANGED"
            };

            var validation = issue.UpdateValues(issueUpdate);

            validation.IsValid.Should().BeTrue();
            issue.ClosedAt.Should().NotBeNull();
            issue.ClosedAt.Should().Be(issueUpdate.ClosedAt);
            issue.Title.Should().Be(issueUpdate.Title);
            issue.Body.Should().Be(issue.Body);
        }
    }
}
