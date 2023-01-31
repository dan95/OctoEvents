using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OctoEvents.Domain.Entities;
using OctoEvents.Infrastructure.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoEvents.Infrastructure.Data.Mappings
{
    public class IssueEventMapping : IEntityTypeConfiguration<IssueEvent>
    {
        public void Configure(EntityTypeBuilder<IssueEvent> builder)
        {
            builder.SetupStandardFields(nameof(IssueEvent));
            builder.Property(x => x.Action).HasColumnName(nameof(IssueEvent.Action)).StandardVarchar();
        }
    }
}
