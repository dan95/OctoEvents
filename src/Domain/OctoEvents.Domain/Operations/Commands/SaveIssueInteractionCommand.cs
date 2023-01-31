using FluentValidation.Results;
using MediatR;
using OctoEvents.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoEvents.Domain.Operations.Commands
{
    public class SaveIssueInteractionCommand : IRequest<ValidationResult>
    {
        public SaveIssueInteractionCommand(EventViewModel @event)
        {
            Event = @event;
        }

        public EventViewModel Event { get; private set; }
    }
}
