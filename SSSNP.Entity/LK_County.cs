using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Entity
{
    [Serializable]
    public class LK_County
    {
        public int CountyID { get; set; }
        public string CountyName { get; set; }
        public string CountyCode { get; set; }
        public int StateID { get; set; }

    }
}
