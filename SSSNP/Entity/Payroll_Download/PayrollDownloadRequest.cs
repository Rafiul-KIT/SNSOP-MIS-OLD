using System;

namespace SSSNP.Entity.Payroll_Download
{
    public class PayrollDownloadRequest
    {
        public int state { get; set; }
        public int county { get; set; }
        public int payam { get; set; }
        public int boma { get; set; }

        public string supportType { get; set; }

        /*public DateTime createdFrom { get; set; }
        public DateTime createdTo { get; set; } 
        public string payrollId { get; set; }*/

    }
}
