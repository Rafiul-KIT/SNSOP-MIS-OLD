using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Core.Model.Component2
{
    public class HouseholdGroupLinkageModel : CommonModel
    {
        public List<string> HouseholdNumbers { get; set; }
        public string HouseholdNumber { get; set; }
        public int GroupId { get; set; }
    }
}
