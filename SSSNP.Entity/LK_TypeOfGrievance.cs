using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Entity
{
    [Serializable]
    public class LK_TypeOfGrievance
    {
        public int TypeOfGrievanceID { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }

    }
}
