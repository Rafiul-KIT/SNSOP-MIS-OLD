using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSSNP.Entity.Payroll
{
    [Serializable]
    public class SavePayrollResponse
    {
        public int? errorCode { get; set; }
        public string errorMsg { get; set; }
        public Boolean operationResult { get; set; }
    }
}