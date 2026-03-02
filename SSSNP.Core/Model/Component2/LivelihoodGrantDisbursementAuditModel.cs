using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Core.Model.Component2
{
    public class LivelihoodGrantDisbursementAuditModel : GenericAuditModel
    {
        public string HouseholdNumber { get; set; }
        public string PaymentCycle { get; set; }
        public decimal? PaymentAmount { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime? ActualPaymentDate { get; set; }
    }
}
