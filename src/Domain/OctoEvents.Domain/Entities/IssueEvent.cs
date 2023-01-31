using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoEvents.Domain.Entities
{
    public class IssueEvent : BaseEntity
    {
        public string Action { get; set; } = string.Empty;
        public Guid SenderId { get; set; }
        public User Sender { get; set; } = new();
        public Guid IssueId { get; set; }
        public Issue Issue { get; set; } = new();
    }
}
