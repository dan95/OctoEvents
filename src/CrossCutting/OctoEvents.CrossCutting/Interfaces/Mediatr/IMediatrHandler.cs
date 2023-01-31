using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoEvents.CrossCutting.Interfaces.Mediatr
{
    public interface IMediatrHandler
    {
        Task<TResponse> SendCommandAsync<TRequest, TResponse>(TRequest command)
            where TRequest : IRequest<TResponse>;
    }
}
