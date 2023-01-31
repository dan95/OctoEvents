using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OctoEvents.CrossCutting.IoC.DI;
using OctoEvents.Tests.Configuration;
using OctoEvents.Tests.Configuration.Fixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace OctoEvents.Infrastructure.Data.IntegrationTests.Schema
{
    public class SchemaValidationTests : BaseTestFixture, IClassFixture<DependencyInjectionFixture>, IDisposable
    {
        private static readonly Action<IServiceCollection> _action = (services) =>
        {
            services.AddSingleton<IConfiguration>(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build());
            services.RegisterDbSettings();
        };

        private bool _disposed;
        private readonly OctoEventsDbContext _context;

        public SchemaValidationTests(DependencyInjectionFixture fixture) : base(fixture, _action)
        {
            _context = _provider.GetRequiredService<OctoEventsDbContext>();
            _context.Database.EnsureDeleted();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            _context.Database.EnsureDeleted();
            _context.Dispose();

            _disposed = true;
        }

        [Fact(DisplayName = "It should create schema against database")]
        public async Task ShouldGenerateDatabase()
        {
            await _context.Database.MigrateAsync(_cancellationTokenSource.Token);

            (await _context.Database
                .SqlQueryRaw<string>("SELECT MigrationId FROM __EFMigrationsHistory")
                .AnyAsync())
            .Should().BeTrue();
        }
    }
}
