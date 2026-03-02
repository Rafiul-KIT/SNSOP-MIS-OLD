using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Entity
{
    [Serializable]
    public class LK_Boma
    {
        public int BomaID { get; set; }
        public string BomaName { get; set; }
        public string BomaCode { get; set; }
        public int PayamID { get; set; }

    }
}
