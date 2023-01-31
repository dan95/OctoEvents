using FluentValidation;
using OctoEvents.Domain.Extensions.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoEvents.Domain.ViewModel.Validators
{
    public class UserViewModelValidator : BaseExternalObjectValidator<UserViewModel>
    {
        public UserViewModelValidator()
        {
            RuleFor(x => x).NotNull();
            RuleFor(x => x.Login).ValidString();
            RuleFor(x => x.RepositoriesUrl).ValidString();
            RuleFor(x => x.Type).ValidString();
        }
    }
}
