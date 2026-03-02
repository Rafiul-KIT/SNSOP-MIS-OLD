using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Entity
{
    [Serializable]
    public class LK_GBVStatus
    {
        public int StatusID { get; set; }
        public string StatusName { get; set; }
        public bool GBVStatus { get; set; }
        public bool NonGBVStatus { get; set; }

    }
}
