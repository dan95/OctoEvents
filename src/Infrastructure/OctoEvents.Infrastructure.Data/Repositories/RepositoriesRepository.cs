using OctoEvents.CrossCutting.Interfaces.Repositories;
using OctoEvents.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoEvents.Infrastructure.Data.Repositories
{
    public class RepositoriesRepository : ExternalEntityBaseRepository<Repository>, IRepositoriesRepository
    {
        public RepositoriesRepository(OctoEventsDbContext dbContext) : base(dbContext)
        {
        }
    }
}
