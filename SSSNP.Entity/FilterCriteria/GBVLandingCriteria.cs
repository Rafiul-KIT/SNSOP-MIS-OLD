using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Entity.FilterCriteria
{
    [Serializable]
    public class GBVLandingCriteria
    {
        public string StatusID { get; set; }
        public string StateID { get; set; }
        public string CountyID { get; set; }
        public string PayamID { get; set; }
        public string BomaID { get; set; }

    }
}
