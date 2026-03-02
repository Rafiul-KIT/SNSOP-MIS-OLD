using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSSNP.UI.Public_Works.Payroll
{
    public class PayrollReconciliationResponse
    {
        public List<BulkResponseDto> bulkResponse;

        public PayrollReconciliationResponse()
        {
            bulkResponse = new List<BulkResponseDto>();
        }
    }
}