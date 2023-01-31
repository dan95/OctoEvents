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
    public static class DbFieldMappingExtensions
    {
        public static PropertyBuilder<Guid> StandardGuid(this PropertyBuilder<Guid> builder)
            => builder.HasColumnType("uniqueidentifier");

        public static PropertyBuilder<Guid?> StandardGuid(this PropertyBuilder<Guid?> builder)
            => builder.HasColumnType("uniqueidentifier");

        public static PropertyBuilder<long> StandardLong(this PropertyBuilder<long> builder)
            => builder.HasColumnType("bigint");

        public static PropertyBuilder<int> StandardInt(this PropertyBuilder<int> builder)
            => builder.HasColumnType("int");

        public static PropertyBuilder<bool> StandardBit(this PropertyBuilder<bool> builder)
            => builder.HasColumnType("bit");

        public static PropertyBuilder<string> StandardVarchar(this PropertyBuilder<string> builder)
            => builder.HasColumnType("varchar(400)");

        public static PropertyBuilder<string> CustomVarchar(this PropertyBuilder<string> builder, int size = 0)
            => (size > 8000 || size <= 0)
            ? builder.HasColumnType("varchar(max)")
            : builder.HasColumnType($"varchar({size})");

        public static PropertyBuilder<DateTime> StandardDateTime(this PropertyBuilder<DateTime> builder)
            => builder.HasColumnType("datetime2");

        public static PropertyBuilder<DateTime?> StandardDateTime(this PropertyBuilder<DateTime?> builder)
            => builder.HasColumnType("datetime2");
    }
}
