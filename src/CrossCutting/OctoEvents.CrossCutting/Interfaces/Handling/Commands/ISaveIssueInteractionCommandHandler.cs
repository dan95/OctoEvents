using FluentValidation.Results;
using MediatR;
using OctoEvents.Domain.Operations.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoEvents.CrossCutting.Interfaces.Handling.Commands
{
    public interface ISaveIssueInteractionCommandHandler : IRequestHandler<SaveIssueInteractionCommand, ValidationResult>
    {
    }
}
