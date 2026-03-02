using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Core.Model.Component2
{
    public class HouseholdGroupLinkageModel : CommonModel
    {
        
            public List<string> HouseholdNumbers { get; set; } // List of household numbers
            public string HouseholdNumber { get; set; } // Single household number for individual use
            public int GroupId { get; set; }
      
    }
}
