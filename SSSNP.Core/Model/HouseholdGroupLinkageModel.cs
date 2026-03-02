using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Core.Model
{
    public class HouseholdGroupLinkageModel
    {
        public string HouseholdNumber { get; set; }
        public int? GroupId { get; set; }
        public string GroupName { get; set; }
    }
}
