using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSSNP.UI.Public_Works.Payroll
{
    public class PayrollReconciliationRequest
    {
        public List<PayrollReconciliationDto> dataList;

        public PayrollReconciliationRequest()
        {
            dataList = new List<PayrollReconciliationDto>();
        }
    }
}