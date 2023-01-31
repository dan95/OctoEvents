using Microsoft.Extensions.DependencyInjection;
using OctoEvents.CrossCutting.IoC.DI;
using OctoEvents.Tests.Configuration.Fixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoEvents.Tests.Configuration
{
    public class BaseTestFixture
    {
        protected readonly DependencyInjectionFixture _fixture;
        protected readonly IServiceProvider _provider;
        protected readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public BaseTestFixture(DependencyInjectionFixture fixture, Action<IServiceCollection>? servicesAction = default)
        {
            _fixture = fixture;

            _provider = _fixture.GetServiceProvider(x =>
            {
                if (servicesAction != null)
                {
                    servicesAction(x);
                }

                x.AddLogging();
                x.RegisterAutoMapper();
                x.RegisterValidators();
            });
        }
    }
}
