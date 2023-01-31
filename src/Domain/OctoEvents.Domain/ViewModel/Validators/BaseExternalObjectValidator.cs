using FluentValidation;
using OctoEvents.Domain.Extensions.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoEvents.Domain.ViewModel.Validators
{
    public abstract class BaseExternalObjectValidator<T> : AbstractValidator<T>, IValidation
        where T : BaseExternalObject
    {
        public BaseExternalObjectValidator()
        {
            RuleFor(x => x).NotNull();
            RuleFor(x => x!.ExternalId).ValidLong();
            RuleFor(x => x!.CreatedAt).ValidDate();
            RuleFor(x => x!.EventsUrl).ValidString();
            RuleFor(x => x!.HtmlUrl).ValidString();
            RuleFor(x => x!.NodeId).ValidString();
            RuleFor(x => x!.Url).ValidString();
        }
    }
}
