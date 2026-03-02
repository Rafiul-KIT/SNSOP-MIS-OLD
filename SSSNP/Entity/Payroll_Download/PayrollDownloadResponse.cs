using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace SSSNP.Entity.Payroll_Download
{
    public class PayrollDownloadResponse
    {
        public bool operationResult { get; set; }
        public string errorMsg { get; set; }
        public int? errorCode { get; set; }
        public List<PDPayrollDataContainer> data { get; set; }
        public int totalCount { get; set; }

    }



    public class PDPayrollDataContainer
    {
        public int id { get; set; }
        public int state { get; set; }
        public int county { get; set; }
        public int payam { get; set; }
        public int boma { get; set; }
        public string lockedStatus { get; set; }

        public List<PDPayrollDataDto> payrollData { get; set; }
    }
}
