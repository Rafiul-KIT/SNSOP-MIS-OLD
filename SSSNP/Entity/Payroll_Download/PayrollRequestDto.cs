using System;

namespace SSSNP.Entity.Payroll_Download
{
    public class PayrollRequestDto
    {
        public int boma { get; set; }
        public int county { get; set; }
        public DateTime createdFrom { get; set; }
        public DateTime createdTo { get; set; }
        public int payam { get; set; }
        public string payrollId { get; set; }
        public int state { get; set; }
    }
}
