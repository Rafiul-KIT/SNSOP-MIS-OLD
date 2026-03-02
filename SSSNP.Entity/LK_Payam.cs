using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Entity
{
    [Serializable]
    public class LK_Payam
    {
        public int PayamID { get; set; }
        public string PayamName { get; set; }
        public string PayamCode { get; set; }
        public int CountyID { get; set; }

    }
}
