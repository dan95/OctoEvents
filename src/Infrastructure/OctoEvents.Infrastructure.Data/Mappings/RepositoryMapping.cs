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
    public class RepositoryMapping : IEntityTypeConfiguration<Repository>
    {
        public void Configure(EntityTypeBuilder<Repository> builder)
        {
            builder.SetupStandardExternalEntityFields(nameof(Repository));
            builder.HasMany(x => x.Issues).WithOne(x => x.Repository).HasForeignKey(x => x.RepositoryId).OnDelete(DeleteBehavior.Cascade);
            builder.Property(x => x.Name).HasColumnName(nameof(Repository.Name))!.StandardVarchar();
            builder.Property(x => x.FullName).HasColumnName(nameof(Repository.FullName))!.StandardVarchar();
            builder.Property(x => x.Private).HasColumnName(nameof(Repository.Private)).StandardBit();
            builder.Property(x => x.HtmlUrl).HasColumnName(nameof(Repository.HtmlUrl))!.StandardVarchar();
            builder.Property(x => x.Description).HasColumnName(nameof(Repository.Description))!.StandardVarchar();
            builder.Property(x => x.Fork).HasColumnName(nameof(Repository.Fork)).StandardBit();
            builder.Property(x => x.Archived).HasColumnName(nameof(Repository.Archived)).StandardBit();
            builder.Property(x => x.Disabled).HasColumnName(nameof(Repository.Disabled)).StandardBit();
            builder.Property(x => x.ForkCount).HasColumnName(nameof(Repository.ForkCount)).StandardLong();
            builder.Property(x => x.ForksUrl).HasColumnName(nameof(Repository.ForksUrl))!.StandardVarchar();
            builder.Property(x => x.CollaboratorsUrl).HasColumnName(nameof(Repository.CollaboratorsUrl))!.StandardVarchar();
            builder.Property(x => x.EventsUrl).HasColumnName(nameof(Repository.EventsUrl))!.StandardVarchar();
            builder.Property(x => x.TagsUrl).HasColumnName(nameof(Repository.TagsUrl))!.StandardVarchar();
            builder.Property(x => x.GitTagsUrl).HasColumnName(nameof(Repository.GitTagsUrl))!.StandardVarchar();
            builder.Property(x => x.ContributorsUrl).HasColumnName(nameof(Repository.ContributorsUrl))!.StandardVarchar();
            builder.Property(x => x.SubscribersUrl).HasColumnName(nameof(Repository.SubscribersUrl))!.StandardVarchar();
            builder.Property(x => x.SubscriptionUrl).HasColumnName(nameof(Repository.SubscriptionUrl))!.StandardVarchar();
            builder.Property(x => x.CommitsUrl).HasColumnName(nameof(Repository.CommitsUrl))!.StandardVarchar();
            builder.Property(x => x.DownloadsUrl).HasColumnName(nameof(Repository.DownloadsUrl))!.StandardVarchar();
            builder.Property(x => x.IssuesUrl).HasColumnName(nameof(Repository.IssuesUrl))!.StandardVarchar();
        }
    }
}
