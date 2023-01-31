using FluentAssertions;
using OctoEvents.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoEvents.Domain.UnitTests.Entities
{
    public class RepositoryTests
    {
        [Fact(DisplayName = "It should not allow issue to be updated by an issue with different external ID")]
        public void ItShouldNotAllowUpdatesFromDifferentIssues()
        {
            var repository = new Repository();
            repository.ExternalId = 123;
            var differentRepository = new Repository();
            differentRepository.ExternalId = 124;

            var validation = repository.UpdateValues(differentRepository);

            validation.IsValid.Should().BeFalse();

            validation.Errors.Should().HaveCount(1);
            validation.Errors[0].PropertyName.Should().Be(nameof(Issue.ExternalId));
            validation.Errors[0].ErrorMessage.Should().Be("The external entity ID must match the updated entity's.");
        }
    }
}
