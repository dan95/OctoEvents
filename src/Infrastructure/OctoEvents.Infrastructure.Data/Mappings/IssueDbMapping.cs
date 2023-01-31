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
    public class IssueDbMapping : IEntityTypeConfiguration<Issue>
    {
        public void Configure(EntityTypeBuilder<Issue> builder)
        {
            builder.SetupStandardExternalEntityFields(nameof(Issue));
            builder.HasMany(x => x.Events).WithOne(x => x.Issue).HasForeignKey(x => x.IssueId).OnDelete(DeleteBehavior.Cascade);
            builder.Property(x => x.Url).HasColumnName(nameof(Issue.Url)).StandardVarchar();
            builder.Property(x => x.RepositoryUrl).HasColumnName(nameof(Issue.RepositoryUrl)).StandardVarchar();
            builder.Property(x => x.LabelsUrl).HasColumnName(nameof(Issue.LabelsUrl)).StandardVarchar();
            builder.Property(x => x.CommentsUrl).HasColumnName(nameof(Issue.CommentsUrl)).StandardVarchar();
            builder.Property(x => x.HtmlUrl).HasColumnName(nameof(Issue.HtmlUrl)).StandardVarchar();
            builder.Property(x => x.NodeId).HasColumnName(nameof(Issue.NodeId)).StandardVarchar();
            builder.Property(x => x.Number).HasColumnName(nameof(Issue.Number)).StandardLong();
            builder.Property(x => x.Title).HasColumnName(nameof(Issue.Title))!.StandardVarchar();
            builder.Property(x => x.State).HasColumnName(nameof(Issue.State)).StandardVarchar();
            builder.Property(x => x.Locked).HasColumnName(nameof(Issue.Locked)).StandardBit();
            builder.Property(x => x.CommentCount).HasColumnName(nameof(Issue.CommentCount)).StandardLong();
            builder.Property(x => x.ClosedAt).HasColumnName(nameof(Issue.ClosedAt)).StandardDateTime();
            builder.Property(x => x.Body).HasColumnName(nameof(Issue.Body))!.CustomVarchar();
            builder.Property(x => x.TimelineUrl).HasColumnName(nameof(Issue.TimelineUrl))!.StandardVarchar();
            builder.Property(x => x.StateReason).HasColumnName(nameof(Issue.StateReason))!.StandardVarchar();
        }
    }
}
