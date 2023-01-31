using FluentValidation;
using OctoEvents.Domain.Extensions.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoEvents.Domain.ViewModel.Validators
{
    public class EventViewModelValidator : AbstractValidator<EventViewModel>, IValidation
    {
        public EventViewModelValidator(
            IValidator<IssueViewModel> issueValidator,
            IValidator<RepositoryViewModel> repositoryValidator,
            IValidator<UserViewModel> userValidator
            )
        {
            RuleFor(x => x).NotNull();
            RuleFor(x => x.Action).ValidString();
            RuleFor(x => x.Issue).NotNull().SetValidator(issueValidator!);
            RuleFor(x => x.Repository).NotNull().SetValidator(repositoryValidator!);
            RuleFor(x => x.Sender).NotNull().SetValidator(userValidator!);
        }
    }
}
