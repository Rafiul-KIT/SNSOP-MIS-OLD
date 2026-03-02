using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSSNP.Entity.Payroll_Download
{
    public class PayrollMergeRequest
    {
        public long boma { get; set; }
        public int county { get; set; }
        public int payam { get; set; }
        public List<string> paymentCycleList { get; set; }
        public int state { get; set; }
        public string supportType { get; set; } // "LIPW", "DIS", or "ALL"
    }
}