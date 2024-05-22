using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSSNP.Entity.Payroll
{
    [Serializable]
    public class PayrollDto
    {
        public List<PayrollDataDto> payrollData { get; set; }

        public PayrollDto()
        {
            payrollData = new List<PayrollDataDto>();
        }
    }
}