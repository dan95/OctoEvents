using FluentValidation;
using OctoEvents.Domain.Extensions.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoEvents.Domain.ViewModel.Validators
{
    public class IssueViewModelValidator : BaseExternalObjectValidator<IssueViewModel>
    {
        public IssueViewModelValidator(
            IValidator<UserViewModel> userValidator
            )
        {
            RuleFor(x => x).NotNull();
            RuleFor(x => x.Title)!.ValidString();
            RuleFor(x => x.Body)!.ValidString();
            RuleFor(x => x.Number)!.ValidLong();
            RuleFor(x => x.User).NotNull().SetValidator(userValidator!);
        }
    }
}
