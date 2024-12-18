using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Core.Model.Component2
{
    public class FinancialServiceLinkageAuditModel : GenericAuditModel
    {
        public string HouseholdNumber { get; set; }
        public Boolean ConnectedFinancialServices { get; set; }
        public string EntityName { get; set; }
        public DateTime? FinancialServiceConnectionDate { get; set; }
        public bool? DepositAccountOpened { get; set; }
        public DateTime? DepositAccountOpeningDate { get; set; }
        public DateTime? AccountLastActivityDate { get; set; }
    }
}
