using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Core.Model.Component2
{
    public class BusinessPlanDevelopmentAuditModel : GenericAuditModel
    {
        public string HouseholdNumber { get; set; }
        public bool? BusinessPlanStatus { get; set; }
        public int? BusinessType { get; set; }

        public bool? PlanSubmitted { get; set; }
        public bool? PlanScreened { get; set; }
        public bool? PlanApproved { get; set; }

        public string WhyRejected { get; set; }
        public string BusinessDescription { get; set; }
        public string HowSpent { get; set; }

        public decimal? InPersonPercentage { get; set; }
        public decimal? GroupPercentage { get; set; }
    }
}
