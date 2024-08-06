using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Entity
{
    [Serializable]
    public class GBVMainExtended: GBVMain
    {
        public string Expired { get; set; }
        public string StateName { get; set; }
        public string CountieName { get; set; }
        public string PayamName { get; set; }
        public string BomaName { get; set; }
        public string StatusName { get; set; }
        public int OrderById { get; set; }
    }
}
