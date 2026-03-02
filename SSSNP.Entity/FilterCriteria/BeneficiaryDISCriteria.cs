using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Entity.FilterCriteria
{
    [Serializable]
    public class BeneficiaryDISCriteria
    {
        public string Year { get; set; }
        public int Month { get; set; }
        public int intStateID { get; set; }
        public int intCountieID { get; set; }
        public int intPayamID { get; set; }
        public int intBomasID { get; set; }
        public short PaymentCycle { get; set; }

    }
}
