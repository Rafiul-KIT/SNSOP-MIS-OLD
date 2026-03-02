using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Entity
{
    [Serializable]
    public class LK_TypeOfComplaint
    {
        public int TypeOfComplaintID { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }

    }
}
