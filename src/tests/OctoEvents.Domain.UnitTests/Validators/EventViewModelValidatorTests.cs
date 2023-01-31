using FluentAssertions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using OctoEvents.Domain.ViewModel;
using OctoEvents.Domain.ViewModel.Validators;
using OctoEvents.Tests.Configuration;
using OctoEvents.Tests.Configuration.Fixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OctoEvents.Domain.UnitTests.Validators
{
    public class EventViewModelValidatorTests : BaseTestFixture, IClassFixture<DependencyInjectionFixture>
    {
        public EventViewModelValidatorTests(DependencyInjectionFixture fixture) : base(fixture)
        {
        }

        [Fact(DisplayName = "Event should be correctly validated")]
        public async Task ItShouldValidateIssueAsync()
        {
            var eventViewModel = new EventViewModel();

            var validator = _provider.GetRequiredService<IValidator<EventViewModel>>();
            var validationResult = await validator.ValidateAsync(eventViewModel);

            validationResult.IsValid.Should().BeFalse();

            validationResult.Errors.Should().HaveCount(4);
            validationResult.Errors.Should().Contain(x => x.ErrorMessage == "'Action' must not be empty.");
            validationResult.Errors.Should().Contain(x => x.ErrorMessage == "'Repository' must not be empty.");
            validationResult.Errors.Should().Contain(x => x.ErrorMessage == "'Issue' must not be empty.");
            validationResult.Errors.Should().Contain(x => x.ErrorMessage == "'Sender' must not be empty.");
        }
    }
}
