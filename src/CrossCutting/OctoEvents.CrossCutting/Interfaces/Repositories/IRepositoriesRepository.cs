using OctoEvents.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoEvents.CrossCutting.Interfaces.Repositories
{
    public interface IRepositoriesRepository : IExternalEntityRepository<Repository>
    {
    }
}
