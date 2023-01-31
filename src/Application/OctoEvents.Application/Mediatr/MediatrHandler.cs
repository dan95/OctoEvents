using MediatR;
using OctoEvents.CrossCutting.Interfaces.Mediatr;

namespace OctoEvents.Application.Mediatr
{
    public class MediatrHandler : IMediatrHandler
    {
        private readonly IMediator _mediator;

        public MediatrHandler(
            IMediator mediator
            )
        {
            _mediator = mediator;
        }

        public async Task<TResponse> SendCommandAsync<TRequest, TResponse>(TRequest command)
            where TRequest : IRequest<TResponse>
            => await _mediator.Send(command);
    }
}
