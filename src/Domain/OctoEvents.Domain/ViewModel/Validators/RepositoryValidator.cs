using FluentValidation;
using OctoEvents.Domain.Extensions.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoEvents.Domain.ViewModel.Validators
{
    public class RepositoryValidator : BaseExternalObjectValidator<RepositoryViewModel>
    {
        public RepositoryValidator(
            IValidator<UserViewModel> userValidator
            )
        {
            RuleFor(x => x).NotNull();
            RuleFor(x => x.Name).ValidString();
            RuleFor(x => x.FullName).ValidString();
            RuleFor(x => x.IssuesUrl)!.ValidString();
            RuleFor(x => x.Owner).NotNull().SetValidator(userValidator!);
        }
    }
}
