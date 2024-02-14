using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Entity
{
    [Serializable]
    public class LK_State
    {
        public int StateID { get; set; }
        public string StateName { get; set; }
        public string StateCode { get; set; }


    }
}
