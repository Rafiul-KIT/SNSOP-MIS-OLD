using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSSNP.Component_2.HouseholdGroupView
{
    public class HouseholdGroup
    {
        public string GroupName { get; set; } // GroupName is a string and can be null
        public decimal? Latitude { get; set; } // Nullable decimal
        public decimal? Longitude { get; set; } // Nullable decimal
    }
}