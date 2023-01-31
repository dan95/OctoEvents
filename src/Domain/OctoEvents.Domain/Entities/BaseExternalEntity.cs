using FluentValidation.Results;
using OctoEvents.Domain.Enum;
using OctoEvents.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoEvents.Domain.Entities
{
    public abstract class BaseExternalEntity : BaseEntity
    {
        public long ExternalId { get; set; }
        public string NodeId { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public DateTime? ExternalCreationDate { get; set; }
        public DateTime? ExternalLastUpdate { get; set; }

        protected ValidationResult UpdateValues(BaseExternalEntity entity, EOperationPerformer operationPerformer = EOperationPerformer.OCTO_EVENTS_API)
        {
            var validationResult = new ValidationResult();

            if(ExternalId != entity.ExternalId)
            {
                validationResult.Errors.Add(new ValidationFailure(nameof(ExternalId), "The external entity ID must match the updated entity's."));
            }

            entity.FillAuditValuesForUpdate();

            return validationResult;
        }
    }
}
