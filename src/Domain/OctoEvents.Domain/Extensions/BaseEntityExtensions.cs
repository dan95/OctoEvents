using OctoEvents.Domain.Entities;
using OctoEvents.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoEvents.Domain.Extensions
{
    public static class BaseEntityExtensions
    {
        public static T FillAuditValuesForCreation<T>(
            this T entity,
            EOperationPerformer operationPerformer = EOperationPerformer.OCTO_EVENTS_API
        )
            where T : BaseEntity
        {
            entity.CreatedAt = DateTime.UtcNow;
            entity.CreatedBy = $"{operationPerformer}_USER";

            return entity;
        }

        public static T FillAuditValuesForUpdate<T>(
            this T entity,
            EOperationPerformer operationPerformer = EOperationPerformer.OCTO_EVENTS_API
        )
            where T : BaseEntity
        {
            entity.UpdatedAt = DateTime.UtcNow;
            entity.UpdatedBy = $"{operationPerformer}_USER";

            return entity;
        }
    }
}
