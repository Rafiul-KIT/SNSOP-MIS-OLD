using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Core.Model.Component2
{
    public class FinancialServiceLinkageTrackingModel : CommonModel
    {
        public int Id { get; set; }
        public string HouseholdNumber { get; set; }
        public Boolean ConnectedFinancialServices { get; set; }
        public string EntityName { get; set; }
    }
}
