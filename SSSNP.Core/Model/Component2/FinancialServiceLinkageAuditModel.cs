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
        public DateTime? fsp_connection_date { get; set; }
        public bool? deposit_account_opened { get; set; }
        public DateTime? deposit_account_open_date { get; set; }
        public DateTime? account_last_activity_date { get; set; }
    }
}
