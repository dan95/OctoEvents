using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OctoEvents.Application.Handlers.Commands;
using OctoEvents.Application.Mediatr;
using OctoEvents.CrossCutting.Interfaces.Mediatr;
using OctoEvents.CrossCutting.Interfaces.Repositories;
using OctoEvents.Domain.Mappers.Profiles;
using OctoEvents.Domain.ViewModel.Validators;
using OctoEvents.Infrastructure.Data;
using OctoEvents.Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OctoEvents.CrossCutting.IoC.DI
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection RegisterApiServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>()
                .AddTransient<IIssueRepository, IssueRepository>()
                .AddTransient<IRepositoriesRepository, RepositoriesRepository>()
                .AddTransient<IUserRepository, UserRepository>();

            return services;
        }

        public static IServiceCollection RegisterMediatrServices(this IServiceCollection services)
        {
            services
                .AddScoped<IMediatrHandler, MediatrHandler>()
                .AddMediatR(typeof(SaveIssueInteractionCommandHandler).GetTypeInfo().Assembly);

            return services;
        }

        public static IServiceCollection RegisterAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(OctoEventsApiProfile).GetTypeInfo().Assembly);

            return services;
        }

        public static IServiceCollection RegisterDbSettings(this IServiceCollection services)
        {
            services.AddDbContext<OctoEventsDbContext>(
                (provider, config) =>
                    config
                    .UseSqlServer(provider.GetRequiredService<IConfiguration>()
                    .GetConnectionString("OctoEventsDbConnectionString")!)
            );

            return services;
        }

        public static IServiceCollection RegisterValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(IValidation).GetTypeInfo().Assembly);

            return services;
        }
    }
}
