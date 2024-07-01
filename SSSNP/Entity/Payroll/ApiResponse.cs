using System;
using System.Collections.Generic;

namespace SSSNP.Entity.Payroll
{
    public class ApiResponse
    {
        public bool operationResult { get; set; }
        public string errorMsg { get; set; }
        public int? errorCode { get; set; }
        public List<PayrollDataContainer> data { get; set; }
    }

    public class PayrollDataContainer
    {
        public int id { get; set; }
        public int state { get; set; }
        public int county { get; set; }
        public int payam { get; set; }
        public int boma { get; set; }
        public List<PayrollDataDto> payrollData { get; set; }
    }
}
