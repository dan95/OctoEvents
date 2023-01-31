using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using OctoEvents.CrossCutting.IoC.DI;
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
    public class EventViewModelMappingTest : BaseTestFixture, IClassFixture<DependencyInjectionFixture>
    {
        public EventViewModelMappingTest(DependencyInjectionFixture fixture) : base(fixture)
        {
        }

        [Fact(DisplayName = "It should map correctly to Issue")]
        public void ItShouldMapCorrectlyToIssue()
        {
            var @event = new EventViewModel
            {
                Action = "closed",
                Sender = new UserViewModel
                {
                    ExternalId = 12332,
                    Login = "dsadlkjsa"
                },
                Issue = new IssueViewModel
                {
                    ExternalId = 12313,
                    User = new UserViewModel
                    {
                        ExternalId = 12332,
                        Login = "dsadlkjsa"
                    }
                },
                Repository = new RepositoryViewModel
                {
                    ExternalId = 21321412,
                    Owner = new UserViewModel
                    {
                        ExternalId = 12332,
                        Login = "dsadlkjsa"
                    }
                }
            };

            var mapper = _provider.GetRequiredService<IMapper>();

            var issue = mapper.Map<Domain.Entities.Issue>(@event);

            issue.ExternalId.Should().Be(12313);
            issue.User.ExternalId.Should().Be(12332);
            issue.Repository.ExternalId.Should().Be(21321412);
            issue.Events[0].Sender.ExternalId.Should().Be(12332);
            issue.Repository.Owner.ExternalId.Should().Be(12332);
        }
    }
}
