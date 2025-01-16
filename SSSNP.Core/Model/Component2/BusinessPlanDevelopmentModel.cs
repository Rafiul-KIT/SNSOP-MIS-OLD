using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Core.Model.Component2
{
    public class BusinessPlanDevelopmentModel : CommonModel
    {
        public int Id { get; set; }
        public string HouseholdNumber { get; set; }
        public Boolean BusinessPlanStatus { get; set; }
        public int BusinessType { get; set; }
    }
}
