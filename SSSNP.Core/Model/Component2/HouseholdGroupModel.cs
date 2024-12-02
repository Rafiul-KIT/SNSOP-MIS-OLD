using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSSNP_SNSOP.Core.Model.Component2
{
    public class HouseholdGroupModel : CommonModel
    {
        public string GroupName { get; set; } // GroupName is a string and can be null
        public decimal? Latitude { get; set; } // Nullable decimal
        public decimal? Longitude { get; set; } // Nullable decimal
        public bool? VSLA_formed { get; set; }
    }
}