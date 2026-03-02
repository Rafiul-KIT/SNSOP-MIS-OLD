using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Core.Model
{
    public class HouseholdDataModel
    {
        public string HouseholdNumber { get; set; }
        public string HouseholdName { get; set; }
        public string Gender { get; set; }
        public int? Age { get; set; }
        public int? GroupId { get; set; }
        public string GroupName { get; set; }
    }
}
