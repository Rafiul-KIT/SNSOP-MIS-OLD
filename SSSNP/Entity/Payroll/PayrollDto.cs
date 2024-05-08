using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSSNP.Entity.Payroll
{
    [Serializable]
    public class PayrollDto
    {
        public Int64 state { get; set; }
        public Int64 county { get; set; }
        public Int64 payam { get; set; }
        public Int64 boma { get; set; }
        public List<PayrollDataDto> payrollData { get; set; }
        public string payrollId { get; set; }

        public PayrollDto()
        {
            payrollData = new List<PayrollDataDto>();
        }
    }
}