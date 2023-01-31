using Microsoft.EntityFrameworkCore;
using OctoEvents.Domain.Entities;
using OctoEvents.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoEvents.Infrastructure.Data
{
    public class OctoEventsDbContext : DbContext
    {
        public OctoEventsDbContext(DbContextOptions<OctoEventsDbContext> options) : base(options)
        {
        }

        public DbSet<Issue> Issues { get; set; }
        public DbSet<IssueEvent> IssueEvents { get; set; }
        public DbSet<Repository> Repositories { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OctoEventsDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
