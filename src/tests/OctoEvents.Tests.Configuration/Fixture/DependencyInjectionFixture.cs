using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoEvents.Tests.Configuration.Fixture
{
    public class DependencyInjectionFixture
    {
        private readonly IServiceCollection _services;

        public DependencyInjectionFixture()
        {
            _services = new ServiceCollection();
        }

        public IServiceProvider GetServiceProvider(Action<IServiceCollection> servicesAction)
        {
            if(servicesAction != null)
            {
                servicesAction(_services);
            }

            return _services.BuildServiceProvider();
        }
    }
}
