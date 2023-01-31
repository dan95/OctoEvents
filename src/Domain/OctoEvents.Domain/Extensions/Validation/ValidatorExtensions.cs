using FluentValidation;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoEvents.Domain.Extensions.Validation
{
    public static class ValidatorExtensions
    {
        public static IRuleBuilderOptions<T, string> ValidString<T>(this IRuleBuilderInitial<T, string> ruleBuilderOptions)
            where T : class
            => ruleBuilderOptions.HasAnyValue();

        public static IRuleBuilderOptions<T, DateTime?> ValidDate<T>(this IRuleBuilderInitial<T, DateTime?> ruleBuilderOptions)
            where T : class
            => ruleBuilderOptions.HasAnyValue().NotEqual(default(DateTime));

        public static IRuleBuilderOptions<T, long> ValidLong<T>(this IRuleBuilderInitial<T, long> ruleBuilderOptions)
            where T : class
            => ruleBuilderOptions.GreaterThan(0L);

        private static IRuleBuilderOptions<TEntity, TProperty> HasAnyValue<TEntity, TProperty>(this IRuleBuilderInitial<TEntity, TProperty> ruleBuilderOptions)
            where TEntity : class
            => ruleBuilderOptions.NotEmpty().NotNull();
    }
}
