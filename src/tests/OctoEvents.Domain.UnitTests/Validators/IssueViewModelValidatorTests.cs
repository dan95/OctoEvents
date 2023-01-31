using FluentAssertions;
using FluentValidation;
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

namespace OctoEvents.Domain.UnitTests.Validators
{
    public class IssueViewModelValidatorTests : BaseTestFixture, IClassFixture<DependencyInjectionFixture>
    {
        public IssueViewModelValidatorTests(DependencyInjectionFixture fixture) : base(fixture)
        {
        }

        [Fact(DisplayName = "Issue should be correctly validated")]
        public async Task ItShouldValidateIssueAsync()
        {
            var issueViewModel = new IssueViewModel();

            var validator = _provider.GetRequiredService<IValidator<IssueViewModel>>();
            var validationResult = await validator.ValidateAsync(issueViewModel);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().HaveCount(11);
            validationResult.Errors.Should().Contain(x => x.ErrorMessage == "'External Id' must be greater than '0'.");
            validationResult.Errors.Should().Contain(x => x.ErrorMessage == "'Events Url' must not be empty.");
            validationResult.Errors.Should().Contain(x => x.ErrorMessage == "'Html Url' must not be empty.");
            validationResult.Errors.Should().Contain(x => x.ErrorMessage == "'Node Id' must not be empty.");
            validationResult.Errors.Should().Contain(x => x.ErrorMessage == "'Url' must not be empty.");
            validationResult.Errors.Should().Contain(x => x.ErrorMessage == "'Title' must not be empty.");
            validationResult.Errors.Should().Contain(x => x.ErrorMessage == "'Body' must not be empty.");
            validationResult.Errors.Should().Contain(x => x.ErrorMessage == "'Number' must be greater than '0'.");
            validationResult.Errors.Should().Contain(x => x.ErrorMessage == "'User' must not be empty.");
        }

        [Fact(DisplayName = "Issue should pass validation")]
        public async Task IssueShouldPassValidation()
        {
            var defaultUrl = "http:localhost";

            var issueViewModel = new IssueViewModel()
            {
                ExternalId = 1231,
                EventsUrl = defaultUrl,
                HtmlUrl = defaultUrl,
                NodeId = "s0djsoDJ9m9pdm9ADPA",
                Url = defaultUrl,
                Title = "TITLE TEST",
                Body = "BODY TEST",
                Number = 23103,
                CreatedAt = new DateTime(2023, 01, 29),
                User = new UserViewModel
                {
                    ExternalId = 321930,
                    Url = defaultUrl,
                    HtmlUrl = defaultUrl,
                    NodeId = "Jpod9p´smipodjas9",
                    Login = "USER",
                    RepositoriesUrl = defaultUrl,
                    Type = "user",
                    CreatedAt = new DateTime(2023, 01, 29),
                    EventsUrl = defaultUrl
                }
            };

            var validator = _provider.GetRequiredService<IValidator<IssueViewModel>>();
            var validationResult = await validator.ValidateAsync(issueViewModel);

            validationResult.IsValid.Should().BeTrue();

            validationResult.Errors.Should().BeEmpty();
        }

    }
}
