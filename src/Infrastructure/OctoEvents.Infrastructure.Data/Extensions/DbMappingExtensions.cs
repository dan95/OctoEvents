using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OctoEvents.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoEvents.Infrastructure.Data.Extensions
{
    public static class DbMappingExtensions
    {
        public static EntityTypeBuilder<T> SetupStandardFields<T>(this EntityTypeBuilder<T> modelBuilder, string entityName, bool shouldMapBaseId = false)
            where T : BaseEntity
        {
            if (shouldMapBaseId)
            {
                modelBuilder.Property(x => x.Id).HasColumnName(nameof(BaseEntity.Id)).StandardGuid();
                modelBuilder.HasKey(x => x.Id).HasName($"PK_{entityName}");
            }

            modelBuilder.Property(x => x.CreatedBy).HasColumnName(nameof(BaseEntity.CreatedBy))!.StandardVarchar();
            modelBuilder.Property(x => x.CreatedAt).HasColumnName(nameof(BaseEntity.CreatedAt)).StandardDateTime();
            modelBuilder.Property(x => x.UpdatedBy).HasColumnName(nameof(BaseEntity.UpdatedBy))!.StandardVarchar();
            modelBuilder.Property(x => x.UpdatedAt).HasColumnName(nameof(BaseEntity.UpdatedAt)).StandardDateTime();

            return modelBuilder;
        }

        public static EntityTypeBuilder<T> SetupStandardExternalEntityFields<T>(this EntityTypeBuilder<T> modelBuilder, string entityName, bool shouldMapBaseId = false)
            where T : BaseExternalEntity
        {
            modelBuilder.SetupStandardFields(entityName, shouldMapBaseId);
            modelBuilder.Property(x => x.ExternalId).HasColumnName(nameof(BaseExternalEntity.ExternalId))!.StandardLong();
            modelBuilder.Property(x => x.Url).HasColumnName(nameof(BaseExternalEntity.Url)).CustomVarchar(2000);
            modelBuilder.Property(x => x.ExternalCreationDate).HasColumnName(nameof(BaseExternalEntity.ExternalCreationDate)).StandardDateTime();
            modelBuilder.Property(x => x.ExternalLastUpdate).HasColumnName(nameof(BaseExternalEntity.ExternalLastUpdate)).StandardDateTime();

            return modelBuilder;
        }
    }
}
